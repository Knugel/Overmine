using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using HarmonyLib;
using Overmine.API.Assets;
using Overmine.API.Assets.References;
using UnityEngine;

namespace Overmine.Patches
{
    public class TaskPatches
    {
        [HarmonyPatch(typeof(JSONDeserialization), "IndexToUnityObject")]
        [HarmonyPrefix]
        public static bool IndexToUnityObject(int index, List<Object> unityObjects, ref Object __result)
        {
            var obj = index < 0 || index >= unityObjects.Count ? null : unityObjects[index];
            __result = obj;

            Object original = null;
            if (obj is IAssetReference reference)
                original = Assets.Resolve<Object>(reference);
                
            if (original != null)
                __result = original;

            return false;
        }
    }
}