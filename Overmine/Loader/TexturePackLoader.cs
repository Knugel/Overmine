using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Logging;
using UnityEngine;

namespace Overmine.Loader
{
    public class TexturePackLoader
    {
        private readonly IDictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private readonly IDictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

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
                LoadTexturePack(directory);
            }

            _logger.LogInfo($"Found {_textures.Count} textures.");
        }

        public Sprite GetSprite(string name, Vector2 pivot, float pixelsPerUnit)
        {
            if (_sprites.TryGetValue(name, out var sprite))
                return sprite;
            if (_textures.TryGetValue(name, out var texture))
            {
                var tmp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelsPerUnit);
                _sprites[name] = tmp;
                return tmp;
            }
            return null;
        }

        public Texture2D GetTexture(string name)
        {
            return _textures.TryGetValue(name, out var texture) ? texture : null;
        }

        private void LoadTexturePack(string directory)
        {
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                var contents = File.ReadAllBytes(file);
                var texture = new Texture2D(1, 1)
                {
                    filterMode = FilterMode.Point
                };
                texture.LoadImage(contents);

                var name = Path.GetFileNameWithoutExtension(file);

                if (_textures.TryGetValue(name, out var existing))
                {
                    Object.Destroy(existing);
   
                }
                
                _textures[name] = texture;
            }
        }
    }
}