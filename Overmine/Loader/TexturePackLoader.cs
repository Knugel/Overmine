using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Logging;
using UnityEngine;

namespace Overmine.Loader
{
    public class TexturePackLoader
    {
        private readonly IDictionary<string, TexturePack> _texturePacks = new Dictionary<string, TexturePack>();
        private readonly ManualLogSource _logger;
        
        public TexturePackLoader(ManualLogSource logger)
        {
            _logger = logger;
        }
        
        public void Load(string path)
        {
            if (!Directory.Exists(path))
                return;

            var directories = Directory.EnumerateDirectories(path).ToList();
            _logger.LogInfo($"Searching {directories.Count} directories at {Path.GetFullPath(path)}.");
            
            foreach (var directory in directories)
            {
                var pack = LoadTexturePack(directory);
                if(pack != null)
                    _texturePacks.Add(directory, pack);
            }

            _logger.LogInfo($"Found {_texturePacks.Count} valid texture packs.");
        }

        public Sprite GetSprite(string name, Vector2 pivot)
        {
            foreach (var pack in _texturePacks.Values)
            {
                if (pack.Sprites.TryGetValue(name, out var sprite))
                    return sprite;
                if (pack.Textures.TryGetValue(name, out var texture))
                {
                    var tmp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot);
                    pack.Sprites[name] = tmp;
                    return tmp;
                }
            }
            return null;
        }

        private TexturePack LoadTexturePack(string directory)
        {
            var pack = new TexturePack
            {
                Textures = new Dictionary<string, Texture2D>(),
                Sprites = new Dictionary<string, Sprite>()
            };

            foreach (var file in Directory.EnumerateFiles(directory))
            {
                var contents = File.ReadAllBytes(file);
                var texture = new Texture2D(1, 1);
                texture.LoadImage(contents);
                pack.Textures.Add(Path.GetFileNameWithoutExtension(file), texture);
            }
            
            return pack;
        }
    }
}