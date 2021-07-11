using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thor;

namespace Overmine.Exporter.Converters
{
    public class EntityConverter : JsonConverter<Entity>
    {
        private static readonly Type[] ToExport = new[]
        {
            typeof(AbilityExt),
            typeof(BehaviorExt),
            typeof(StatusEffectExt),
            typeof(HealthExt),
            typeof(DamageExt),
            typeof(LootExt)
        };
        
        public override void WriteJson(JsonWriter writer, Entity value, JsonSerializer serializer)
        {
            var obj = new JObject();
            obj.Add("Name", value.name);

            CustomResolver.WriteFields(value, obj, serializer);

            var isDirty = false;
            foreach (var extensions in value.gameObject.GetComponents<Extension>().GroupBy(x => x.GetType()))
            {
                // if (ToExport.Contains(extensions.Key))
                // {
                    obj.Add(extensions.Key.Name, JToken.FromObject(extensions, serializer));
                    isDirty = true;
                // }
            }
            
            if(isDirty)
                obj.WriteTo(writer);
        }

        public override Entity ReadJson(JsonReader reader, Type objectType, Entity existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}