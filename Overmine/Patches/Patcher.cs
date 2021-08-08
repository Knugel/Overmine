using HarmonyLib;

namespace Overmine.Patches
{
    public static class Patcher
    {
        public const string HarmonyInstanceID = "com.knugel.overmine";
        
        public static void Install()
        {
           Patch<TaskPatches>();
           Patch<LocalizerPatches>();
           Patch<GameDataPatches>();
           Patch<PopupPatches>();
           Patch<RoomPatches>();
           Patch<AudioExtPatches>();

           Patch<CollectorRoomPatches>();
        }

        private static void Patch<T>()
        {
            Harmony.CreateAndPatchAll(typeof(T), HarmonyInstanceID);
        }
    }
}