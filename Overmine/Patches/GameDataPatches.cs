using HarmonyLib;
using Overmine.API.Events;
using Thor;

namespace Overmine.Patches
{
    public class GameDataPatches
    {
        
        [HarmonyPatch(typeof(GameData), nameof(GameData.Setup))]
        [HarmonyPrefix]
        public static void PreSetup()
        {
            GameEvents.Fire(new SetupEvent.Pre(GameData.Instance));
        }
        
        [HarmonyPatch(typeof(GameData), nameof(GameData.Setup))]
        [HarmonyPostfix]
        public static void PostSetup()
        {
            GameEvents.Fire(new SetupEvent.Post(GameData.Instance));
        }
    }
}