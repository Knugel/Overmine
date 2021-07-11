using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thor;

namespace Overmine.Exporter.Converters
{
    public class LocIDConverter : JsonConverter<LocID>
    {
        public override void WriteJson(JsonWriter writer, LocID value, JsonSerializer serializer)
        {
            var obj = new JObject();
            obj.Add("Key", value.Id);
            obj.Add("Value", Localizer.Instance.GetLocString(value));
            obj.WriteTo(writer);
        }

        public override LocID ReadJson(JsonReader reader, Type objectType, LocID existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}