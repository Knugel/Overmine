using BepInEx;
using Overmine.API.Events;
using Overmine.Loader;
using Overmine.Patches;

namespace Overmine
{
    [BepInPlugin("com.knugel.overmine", "Overmine", "0.1.0")]
    public class Overmine : BaseUnityPlugin
    {
        public static Overmine Instance { get; private set; }
        public ModLoader Loader { get; }

        public Overmine()
        {
            Instance = this;
            Patcher.Install();

            Loader = new ModLoader(Logger);
            GameEvents.Register<SetupEvent.Pre>(OnSetup);
        }

        private void OnSetup(SetupEvent.Pre ev)
        {
            Loader.Load("Mods");
            Loader.InitializeMods();
        }
    }
}