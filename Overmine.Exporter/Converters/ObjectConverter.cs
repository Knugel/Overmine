using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thor;

namespace Overmine.Exporter.Converters
{
    public class ObjectConverter : JsonConverter
    {
        private static readonly Type[] Excluded =
        {
            typeof(Entity),
            typeof(ExtendedExternalBehaviorTree),
            typeof(LocID)
        };
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = new JObject();
            CustomResolver.WriteFields(value, obj, serializer);
            obj.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return !Excluded.Any(x => x.IsAssignableFrom(objectType)) && !objectType.Namespace.Contains("System");
        }
    }
}