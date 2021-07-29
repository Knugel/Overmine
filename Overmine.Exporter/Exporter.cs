using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BehaviorDesigner.Runtime.Tasks;
using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Overmine.Exporter.Converters;
using Thor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Color = System.Drawing.Color;

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

            var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            var entities = gameObjects
                .Select(x => x.GetComponent<Entity>())
                .Where(x => x != null)
                .GroupBy(x => new { x.name, x.Guid })
                .Select(x => x.First())
                .OrderBy(x => x.Guid)
                .ToList();
            
            Write(entities, "Entities", serializer);
            
            var dObjects = Resources
                .FindObjectsOfTypeAll<DataObject>()
                .Where(x => ToExport.Any(y => y.IsInstanceOfType(x)))
                .OrderBy(x => x.guid);
            Write(dObjects.ToList(), "DataObjects", serializer);
            
            BehaviorConverter.Write = true;
            Write(BehaviorConverter.Behaviors.Values.OrderBy(x => x.GetHashCode()).ToList(), "Behaviors", serializer);
            
            var grids = gameObjects
                .Select(x => x.GetComponent<Grid>())
                .Where(x => x != null)
                .Distinct()
                .ToList();
            
            var field = typeof(Grid).GetField("m_source", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var cells = new Dictionary<Color, GridData>();
            
            foreach (var grid in grids)
            {
                var img = new Bitmap(grid.Width, grid.Height);
                var source = field.GetValue(grid) as Grid.GridMap;

                for (var x = 0; x < grid.Width; x++)
                {
                    for (var y = 0; y < grid.Height; y++)
                    {
                        var cell = source[x, y];
                        var color = Color.FromArgb((int) (cell.Color.a * 255), (int) (cell.Color.r * 255), (int) (cell.Color.g * 255), (int) (cell.Color.b * 255));
                        cells[color] = cell;
                        img.SetPixel(x, y, color);
                    }
                }
                
                img.Save($"Overmine/Grids/{grid.gameObject.name}.png");
            }
            
            Write(cells.ToList(), "Cells", serializer);
        }

        private static void Write<T>(IEnumerable<T> source, string name, JsonSerializer serializer)
        {
            var sb = new StringBuilder(); 
            serializer.Serialize(new StringWriter(sb), source);
            File.WriteAllText($"Overmine/{name}.json", sb.ToString());
        }
    }
}