using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Thor;
using UnityEngine;

namespace Overmine.Exporter
{
    class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var serialize = true;

            if (member.MemberType == MemberTypes.Property)
                serialize = false;
            else if (member is FieldInfo field && field.IsPrivate && field.GetCustomAttribute<SerializeField>() == null)
                serialize = false;

            prop.ShouldSerialize = _ => serialize;

            return prop;
        }

        public static void WriteFields(object value, JObject container, JsonSerializer serializer)
        {
            foreach (var field in GetFields(value.GetType()))
            {
                var fValue = field.GetValue(value);
                var name = Beautify(field.Name);

                if (container.ContainsKey(name))
                {
                    // Debug.LogWarning($"Couldn't add {name} to container of Type {value.GetType().Name}");
                    continue;
                }

                switch (fValue)
                {
                    case Entity entity:
                        container.Add(name, new JObject
                        {
                            {"$Ref", entity.Guid}
                        });
                        break;
                    case DataObject dataObject:
                        container.Add(name, new JObject
                        {
                            {"$Ref", dataObject.guid}
                        });
                        break;
                    default:
                    {
                        if (fValue != null)
                        {
                            container.Add(name, JToken.FromObject(fValue, serializer));
                        }
                        break;
                    }
                }
            }
        }
        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            var ret = new List<FieldInfo>();
            var toCheck = type;
            
            do
            {
                ret.AddRange(toCheck
                    .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(x => x.IsPublic || x.GetCustomAttribute<SerializeField>() != null));
                toCheck = toCheck.BaseType;
            } while (toCheck != typeof(DataObject) && toCheck != typeof(UnityEngine.Object) && toCheck != null);

            return ret;
        }


        private static string Beautify(string name)
        {
            name = name.Replace("m_", "");
            name = char.ToUpper(name[0]) + name.Substring(1);
            return name;
        }
    }
}