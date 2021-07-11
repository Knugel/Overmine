using HarmonyLib;
using Overmine.API.Events;
using Thor;
using UnityEngine;

namespace Overmine.Patches
{
    public class PopupPatches
    {
        [HarmonyPatch(typeof(PopupManager), "CreatePopup")]
        [HarmonyPrefix]
        public static bool PreCreatePopup(ref Popup __result, Popup prefab, object data, Entity owner, Vector3 position, Entity follow = null)
        {
            var ev = new PopupEvent.Create.Pre(prefab, data, owner, position, follow);
            GameEvents.Fire(ev);
            if (ev.Result != null)
                __result = ev.Result;
            return !ev.IsCanceled;
        }
        
        [HarmonyPatch(typeof(PopupManager), "CreatePopup")]
        [HarmonyPostfix]
        public static void PostCreatePopup(ref Popup __result, Popup prefab, object data, Entity owner, Vector3 position, Entity follow = null)
        {
            var ev = new PopupEvent.Create.Post(prefab, data, owner, position, follow, __result);
            GameEvents.Fire(ev);
            if (ev.Result != null)
                __result = ev.Result;
        }
    }
}