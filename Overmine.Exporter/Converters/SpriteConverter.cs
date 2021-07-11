using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Overmine.Exporter.Converters
{
    public class SpriteConverter : JsonConverter<Sprite>
    {
        public static Dictionary<int, Sprite> Sprites = new Dictionary<int, Sprite>();
        public static bool Write = false;
        
        public override void WriteJson(JsonWriter writer, Sprite value, JsonSerializer serializer)
        {
            if (!Write)
            {
                writer.WriteValue(value.GetInstanceID());
                Sprites[value.GetInstanceID()] = value;
            }
            else
            {
                var texture = GetTexture(value.texture);
                if (texture == null)
                    return;

                var croppedTexture = new Texture2D( (int)value.rect.width, (int)value.rect.height );
                var pixels = texture.GetPixels((int)value.textureRect.x, 
                    (int)value.textureRect.y, 
                    (int)value.textureRect.width, 
                    (int)value.textureRect.height );
                croppedTexture.SetPixels(pixels);
                croppedTexture.Apply();

                var png = croppedTexture.EncodeToPNG();

                var obj = new JObject()
                {
                    {"ID", value.GetInstanceID()},
                    {"Data", Convert.ToBase64String(png)}
                };
                obj.WriteTo(writer);
            }
        }

        private Texture2D GetTexture(Texture2D texture)
        {
            Color32[] pixelBlock;
            var img = texture;
                
            try
            {
                pixelBlock = img.GetPixels32();
                return img;
            }
            catch (UnityException)
            {
                img.filterMode = FilterMode.Point;
                var rt = RenderTexture.GetTemporary(img.width, img.height);
                rt.filterMode = FilterMode.Point;
                RenderTexture.active = rt;
                Graphics.Blit(img, rt);
                var img2 = new Texture2D(img.width, img.height);
                img2.ReadPixels(new Rect(0, 0, img.width, img.height), 0, 0);
                img2.Apply();
                RenderTexture.active = null;
                img = img2;
                pixelBlock = img.GetPixels32();
                
                var tmp = new Texture2D(img2.width, img2.height);
                tmp.SetPixels32(pixelBlock);
            }
            return null;
        }

        public override Sprite ReadJson(JsonReader reader, Type objectType, Sprite existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}