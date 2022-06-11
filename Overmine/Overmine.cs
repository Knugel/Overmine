using System.Diagnostics;
using BepInEx;
using HarmonyLib;
using Overmine.API.Events;
using Overmine.Loader;
using Overmine.Patches;
using Thor;
using UnityEngine;
using UnityEngine.UI;

namespace Overmine
{
    [BepInPlugin("com.knugel.overmine", "Overmine", "0.1.3")]
    public class Overmine : BaseUnityPlugin
    {
        public static Overmine Instance { get; private set; }
        public ModLoader Loader { get; }
        public TexturePackLoader TexturePackLoader { get; }

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Overmine()
        {
            Instance = this;
            Logger.LogInfo("Installing Patches");
            Patcher.Install(Logger);

            Loader = new ModLoader(Logger);
            TexturePackLoader = new TexturePackLoader(Logger);
            
            GameEvents.Register<SetupEvent.Pre>(PreSetup);
            GameEvents.Register<SetupEvent.Post>(PostSetup);
        }

        private void PreSetup(SetupEvent.Pre ev)
        {
            Logger.LogInfo("Loading Mods");
            Loader.Load("Mods");
            Loader.InitializeMods();
            
            Logger.LogInfo("Loading Texture Packs");
            TexturePackLoader.Load("Texture Packs");
        }
        
        private void PostSetup(SetupEvent.Post ev)
        {
            ApplyItemTexturePack(ev.Instance);
            ApplyUITexturePack();
            GameEvents.Register(SimulationEvent.EventType.RoomChanged, OnRoomChanged);
        }

        private void OnRoomChanged(SimulationEvent ev)
        {
            ApplyRendererTexturePack();
            ApplyParticlesTexturePack();
        }

        private void ApplyItemTexturePack(GameData instance)
        {
            _stopwatch.Start();
            
            var items = instance.Items;
            foreach (var item in items)
            {
                var sprite = item.Icon;
                if(sprite == null)
                    continue;
                var pivot = GetSpritePivot(sprite);
                var replacement = TexturePackLoader.GetSprite(sprite.name, pivot, sprite.pixelsPerUnit);
                if (replacement != null)
                    new Traverse(item).Field("m_icon").SetValue(replacement);
            }
            
            var elapsed = _stopwatch.Elapsed;
            _stopwatch.Reset();
            Logger.LogInfo($"Applying item textures took {elapsed}");
        }

        private void ApplyRendererTexturePack()
        {
            _stopwatch.Start();
            
            var renderers = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                var sprite = renderer.sprite;
                if(sprite == null)
                    continue;
                var pivot = GetSpritePivot(sprite);
                var replacement = TexturePackLoader.GetSprite(sprite.name, pivot, sprite.pixelsPerUnit);
                if (replacement != null)
                    renderer.sprite = replacement;
            }

            var elapsed = _stopwatch.Elapsed;
            _stopwatch.Reset();
            Logger.LogInfo($"Applying sprite-renderer textures took {elapsed}");
        }

        private void ApplyParticlesTexturePack()
        {
            _stopwatch.Start();
            
            var particles = Resources.FindObjectsOfTypeAll<ParticleSystem>();
            foreach (var particle in particles)
            {
                var renderer = particle.GetComponent<Renderer>();
                if(renderer == null || renderer.sharedMaterial == null)
                    continue;

                var textures = renderer.sharedMaterial.GetTexturePropertyNames();
                foreach (var texture in textures)
                {
                    var current = renderer.sharedMaterial.GetTexture(texture);
                    if(current == null)
                        continue;
                    
                    var replacement = TexturePackLoader.GetTexture(current.name);
                    if (replacement != null)
                    {
                        renderer.sharedMaterial.SetTexture(texture, replacement);
                    }
                }
            }
            var elapsed = _stopwatch.Elapsed;
            _stopwatch.Reset();
            Logger.LogInfo($"Applying particles textures took {elapsed}");
        }

        private void ApplyUITexturePack()
        {
            _stopwatch.Start();
            
            var images = Resources.FindObjectsOfTypeAll<Image>();
            foreach (var image in images)
            {
                var sprite = image.sprite;
                if(sprite == null)
                    continue;
                var pivot = GetSpritePivot(sprite);
                var replacement = TexturePackLoader.GetSprite(sprite.name, pivot, sprite.pixelsPerUnit);
                if (replacement != null)
                    image.sprite = replacement;
            }
            var elapsed = _stopwatch.Elapsed;
            _stopwatch.Reset();
            Logger.LogInfo($"Applying particles textures took {elapsed}");
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