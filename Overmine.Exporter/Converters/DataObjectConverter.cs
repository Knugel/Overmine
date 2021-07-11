using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thor;

namespace Overmine.Exporter.Converters
{
    public class DataObjectConverter : JsonConverter<DataObject>
    {
        public override void WriteJson(JsonWriter writer, DataObject value, JsonSerializer serializer)
        {
            var obj = new JObject();
            obj.Add("Name", value.name);
            CustomResolver.WriteFields(value, obj, serializer);
            obj.WriteTo(writer);
        }

        public override DataObject ReadJson(JsonReader reader, Type objectType, DataObject existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}