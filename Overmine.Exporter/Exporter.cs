using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Overmine.Exporter.Converters;
using Thor;
using UnityEngine;

namespace Overmine.Exporter
{
    [BepInPlugin("com.knugel.overmine.exporter", "Overmine.Exporter", "0.1.0")]
    public class Exporter : BaseUnityPlugin
    {
        private static readonly Type[] ToExport =
        {
            typeof(EntityData),
            typeof(ResourceData),
            typeof(LootTableData),
            typeof(EquipmentData)
        };

        private static readonly Type[] PeonExtensions =
        {
            typeof(VisualExt),
            typeof(MoverExt),
            typeof(FacingExt),
            typeof(CasterExt),
            typeof(HealthExt),
            typeof(DamageExt),
            typeof(InventoryExt),
            typeof(BehaviorExt),
            typeof(HazardAgentExt),
        };
        
        public Exporter()
        {
            Harmony.CreateAndPatchAll(typeof(Exporter), "com.knugel.overmine.exporter");
        }
        
        [HarmonyPatch(typeof(ExternalBehaviorRunner), "Initialize")]
        [HarmonyPostfix]
        public static void OnSetup()
        {
            ExportDataObjects();
        }

        private static void ExportDataObjects()
        {
            var serializer = JsonSerializer.CreateDefault();
            serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializer.ContractResolver = new CustomResolver();
            
            serializer.Converters.Add(new StringEnumConverter());
            serializer.Converters.Add(new BehaviorConverter());
            serializer.Converters.Add(new EntityConverter());
            serializer.Converters.Add(new LocIDConverter());
            serializer.Converters.Add(new DataObjectConverter());
            serializer.Converters.Add(new ObjectConverter());

            if (!File.Exists("Overmine/Entities"))
            {
                var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                var entities = gameObjects
                    .Select(x => x.GetComponent<Entity>())
                    .Where(x => x != null)
                    .GroupBy(x => new { x.name, x.Guid })
                    .Select(x => x.First())
                    .OrderBy(x => x.Guid)
                    .ToList();

                Write(entities, "Entities", serializer);
            }

            if (!File.Exists("Overmine/DataObjects"))
            {
                var dObjects = Resources
                    .FindObjectsOfTypeAll<DataObject>()
                    .Where(x => ToExport.Any(y => y.IsInstanceOfType(x)))
                    .OrderBy(x => x.guid);
                Write(dObjects.ToList(), "DataObjects", serializer);
            }

            if (!Directory.Exists("Overmine/Sprites"))
            {
                Directory.CreateDirectory("Overmine/Sprites");
                var groups = Resources.FindObjectsOfTypeAll<Sprite>().GroupBy(x => x.texture);
                var tmp = new Texture2D(1, 1);

                foreach (var group in groups)
                {
                    var copy = CopyTexture(group.Key);

                    foreach (var sprite in group)
                    {
                        var pixels = copy.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y,
                            (int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        tmp.Resize((int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        tmp.SetPixels(pixels);
                        tmp.Apply();

                        var png = tmp.EncodeToPNG();
                        File.WriteAllBytes($"Overmine/Sprites/{sprite.name}.png", png.ToArray());
                    }

                    Destroy(copy);
                }
                Destroy(tmp);
            }

            if (!Directory.Exists("Overmine/Textures"))
            {
                Directory.CreateDirectory("Overmine/Textures");
                var textures = Resources.FindObjectsOfTypeAll<Texture2D>();
                var index = 0;
                foreach (var texture in textures)
                {
                    var copy = CopyTexture(texture);
                    var buffer = copy.GetRawTextureData<Color32>();
                    var png = ImageConversion.EncodeNativeArrayToPNG(buffer, copy.graphicsFormat,
                        (uint)texture.width,
                        (uint)texture.height);
                    var name = string.IsNullOrEmpty(texture.name) ? $"Texture_{index++}" : texture.name;
                    File.WriteAllBytes($"Overmine/Textures/{name}.png", png.ToArray());
                    Destroy(copy);
                }
            }

            if (!File.Exists("Overmine/Behaviors"))
            {
                BehaviorConverter.Write = true;
                Write(BehaviorConverter.Behaviors.Values.OrderBy(x => x.GetHashCode()).ToList(), "Behaviors",
                    serializer);
            }
        }

        private static void Write<T>(IEnumerable<T> source, string name, JsonSerializer serializer)
        {
            var sb = new StringBuilder(); 
            serializer.Serialize(new StringWriter(sb), source);
            File.WriteAllText($"Overmine/{name}.json", sb.ToString());
        }
        
        private static Texture2D CopyTexture(Texture2D source)
        {
            var renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);
 
            Graphics.Blit(source, renderTex);
            var previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            var readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }

        private class ModifiableMember
        {
            public string Name { get; set; }
            
            public string Type { get; set; }
            
            public string[] Values { get; set; }
        }
    }
}