using System.Collections.Generic;
using UnityEngine;

namespace Overmine.Loader
{
    public class TexturePack
    {
        public IDictionary<string, Texture2D> Textures { get; set; }

        public IDictionary<string, Sprite> Sprites { get; set; }
    }
}