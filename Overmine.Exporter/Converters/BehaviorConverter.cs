using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thor;
using UnityEngine;

namespace Overmine.Exporter.Converters
{
    public class BehaviorConverter : JsonConverter<ExtendedExternalBehaviorTree>
    {
        public static Dictionary<int, ExtendedExternalBehaviorTree> Behaviors = new Dictionary<int, ExtendedExternalBehaviorTree>();
        public static bool Write = false;
        
        public override void WriteJson(JsonWriter writer, ExtendedExternalBehaviorTree value, JsonSerializer serializer)
        {
            if (!Write)
            {
                var reference = new JObject
                {
                    {"$Ref", value.GetInstanceID()}
                };
                Behaviors[value.GetInstanceID()] = value;
                reference.WriteTo(writer);
                return;
            }
            
            var obj = new JObject
            {
                {"Name", value.name},
                {"ID", value.GetInstanceID()},
                {"Interruptable", value.Interruptable},
                {"AutoStart", value.AutoStart},
                {"PreProcess", value.PreProcess},
                {"PauseWhenDisabled", value.PauseWhenDisabled},
                {"RestartWhenComplete", value.RestartWhenComplete}
            };


            if (value.BehaviorSource != null)
            {
                try
                {
                    var json = JToken.Parse(value.BehaviorSource.TaskData.JSONSerialization)
                        .ToString(Formatting.Indented);
                    obj.Add("TaskJSON", json);
                }
                catch (Exception)
                {
                    obj.Add("TaskJSON", value.BehaviorSource.TaskData.JSONSerialization);
                }

                var objects = value.BehaviorSource.TaskData.fieldSerializationData.unityObjects;
                var array = new JArray();
                
                foreach (var uObject in objects.Where(uObject => uObject != null))
                {
                    switch (uObject)
                    {
                        case DataObject dataObject:
                            array.Add(new JObject
                            {
                                {"$Ref", dataObject.guid}
                            });
                            break;
                        case MonoBehaviour behaviour:
                        {
                            var entity = behaviour.GetComponent<Entity>();
                            if (entity != null)
                            {
                                array.Add(new JObject
                                {
                                    {"$Ref", entity.Guid},
                                    {"Component", behaviour.GetType().Name}
                                });
                            }
                            break;
                        }
                        case ExtendedExternalBehaviorTree tree:
                        {
                            array.Add(new JObject
                            {
                                {"$Ref", tree.GetInstanceID()}
                            });
                            Behaviors[tree.GetInstanceID()] = tree;
                            break;
                        }
                        default:
                        {
                            array.Add(new JObject
                            {
                                {"$Ref", uObject.GetInstanceID()}
                            });
                            break;
                        }
                    }
                }
                obj.Add("Objects", array);
            }

            obj.WriteTo(writer);
        }

        public override ExtendedExternalBehaviorTree ReadJson(JsonReader reader, Type objectType, ExtendedExternalBehaviorTree existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}