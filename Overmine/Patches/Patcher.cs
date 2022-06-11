using BepInEx.Logging;
using HarmonyLib;

namespace Overmine.Patches
{
    public static class Patcher
    {
        public const string HarmonyInstanceID = "com.knugel.overmine";
        
        public static void Install(ManualLogSource logger)
        {
           Patch<TaskPatches>(logger);
           Patch<LocalizerPatches>(logger);
           Patch<GameDataPatches>(logger);
           Patch<PopupPatches>(logger);
           Patch<RoomPatches>(logger);
           // Patch<ObjectPatches>(logger);
           // Patch<AudioExtPatches>();

           Patch<CollectorRoomPatches>(logger);
        }

        private static void Patch<T>(ManualLogSource logger)
        {
            logger.LogInfo($"Patch: {typeof(T).Name}");
            Harmony.CreateAndPatchAll(typeof(T), HarmonyInstanceID);
        }
    }
}