using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using HarmonyLib;
using Overmine.API;
using Overmine.API.Assets;
using Overmine.API.Assets.References;
using Thor;
using UnityEngine;

namespace Overmine.Patches
{
    public class TaskPatches
    {
        private static readonly Dictionary<string, DataObject> DataObjects = new Dictionary<string, DataObject>();
        private static readonly Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();

        [HarmonyPatch(typeof(JSONDeserialization), "IndexToUnityObject")]
        [HarmonyPrefix]
        public static bool IndexToUnityObject(int index, List<UnityEngine.Object> unityObjects, ref UnityEngine.Object __result)
        {
            var obj = index < 0 || index >= unityObjects.Count ? null : unityObjects[index];
            __result = obj;

            Object original = null;
            if (obj is DataObjectReference || obj is ResourceReference)
            {
                if (DataObjects.Count == 0)
                    GetDataObjects();

                original = DataObjects[(obj as DataObject).guid];
            }

            if (obj is EntityReference reference)
            {
                if (Entities.Count == 0)
                    GetEntities();

                original = DataObjects[reference.Guid];
            }
            
            if (original != null)
                __result = original;

            return false;
        }

        private static void GetDataObjects()
        {
            var resources = Resources
                .FindObjectsOfTypeAll<DataObject>()
                .Where(x => !(x is ResourceReference || x is DataObjectReference));
            foreach (var resource in resources)
            {
                if (DataObjects.ContainsKey(resource.guid))
                    continue;
                DataObjects.Add(resource.guid, resource);
            }
        }

        private static void GetEntities()
        {
            var entities = Resources
                .FindObjectsOfTypeAll<GameObject>()
                .Select(x => x.GetComponent<Entity>())
                .Where(x => x != null && !(x is EntityReference));
            foreach (var entity in entities)
            {
                if (Entities.ContainsKey(entity.Guid))
                    continue;
                Entities.Add(entity.Guid, entity);
            }
        }
    }
}