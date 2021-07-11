using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        }

        private static void Write(IEnumerable<object> source, string name, JsonSerializer serializer)
        {
            var sb = new StringBuilder(); 
            serializer.Serialize(new StringWriter(sb), source);
            File.WriteAllText($"Overmine/{name}.json", sb.ToString());
        }
    }
}