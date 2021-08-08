using HarmonyLib;
using Overmine.API.Events;
using Thor;

namespace Overmine.Patches
{
    public class RoomPatches
    {
        [HarmonyPatch(typeof(Room), "Setup")]
        [HarmonyPrefix]
        public static bool PreRoomSetup(ref Room __instance)
        {
            var ev = new RoomEvent.Setup.Pre(__instance);
            GameEvents.Fire(ev);
            return !ev.IsCanceled;
        }
        
        [HarmonyPatch(typeof(Room), "Setup")]
        [HarmonyPostfix]
        public static void PostRoomSetup(ref Room __instance)
        {
            var ev = new RoomEvent.Setup.Post(__instance);
            GameEvents.Fire(ev);
        }
    }
}