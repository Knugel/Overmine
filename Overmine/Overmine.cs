using BepInEx;
using HarmonyLib;
using Overmine.API.Events;
using Overmine.Loader;
using Overmine.Patches;
using Thor;
using UnityEngine;

namespace Overmine
{
    [BepInPlugin("com.knugel.overmine", "Overmine", "0.1.3")]
    public class Overmine : BaseUnityPlugin
    {
        public static Overmine Instance { get; private set; }
        public ModLoader Loader { get; }
        public TexturePackLoader TexturePackLoader { get; }

        public Overmine()
        {
            Instance = this;
            Logger.LogInfo("Installing Patches");
            Patcher.Install(Logger);

            Loader = new ModLoader(Logger);
            TexturePackLoader = new TexturePackLoader(Logger);
            GameEvents.Register<SetupEvent.Pre>(OnSetup);
            GameEvents.Register<RoomEvent.Setup.Post>(OnTitleRoom);
        }

        private void OnSetup(SetupEvent.Pre ev)
        {
            Logger.LogInfo("Loading Mods");
            Loader.Load("Mods");
            Loader.InitializeMods();
            
            Logger.LogInfo("Loading Texture Packs");
            TexturePackLoader.Load("Texture Packs");
        }

        private void OnTitleRoom(RoomEvent.Setup.Post ev)
        {
            var renderers = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                var sprite = renderer.sprite;
                if(sprite == null)
                    continue;
                var pivot = GetSpritePivot(sprite);
                var replacement = TexturePackLoader.GetSprite(sprite.name, pivot);
                if (replacement != null)
                    renderer.sprite = replacement;
            }

            var items = GameData.Instance.Items;
            foreach (var item in items)
            {
                var sprite = item.Icon;
                if(sprite == null)
                    continue;
                var pivot = GetSpritePivot(sprite);
                var replacement = TexturePackLoader.GetSprite(sprite.name, pivot);
                if (replacement != null)
                    new Traverse(item).Field("m_icon").SetValue(replacement);
            }
        }
        
        private static Vector2 GetSpritePivot(Sprite sprite)
        {
            var bounds = sprite.bounds;
            var pivotX = - bounds.center.x / bounds.extents.x / 2 + 0.5f;
            var pivotY = - bounds.center.y / bounds.extents.y / 2 + 0.5f;
 
            return new Vector2(pivotX, pivotY);
        }
    }
}