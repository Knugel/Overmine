using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Overmine.API.Assets.References;
using Overmine.API.Assets.Resolvers;
using UnityEngine;

namespace Overmine.API.Assets
{
    public static class Assets
    {
        private static readonly List<IAssetResolver> Resolvers = new List<IAssetResolver>();

        static Assets()
        {
            Resolvers.Add(new DataObjectResolver());
            Resolvers.Add(new PopupResolver());
            Resolvers.Add(new BehaviorTreeResolver());
            
            Resolvers.Add(new EntityResolver());
            Resolvers.Add(new NamedResolver());
        }

        public static T Resolve<T>(IAssetReference reference) where T : class
        {
            if (reference == null)
                return null;
            
            var resolver = Resolvers.FirstOrDefault(x => x.CanResolve(reference.GetType()));
            return resolver?.Resolve(reference) as T;
        }

        public static UnityEngine.Object Resolve(IAssetReference reference)
        {
            return Resolve<UnityEngine.Object>(reference);
        }

        public static bool CanResolve(Type type)
        {
            return Resolvers.Any(x => x.CanResolve(type));
        }

        public static void ResolveComponent(object component)
        {
            var fields = GetSerializedFields(component.GetType());
            foreach (var field in fields)
            {
                var value = field.GetValue(component);
                if(value == null)
                    continue;
                
                if (value is IList collection)
                {
                    for (var i = 0; i < collection.Count; i++)
                    {
                        var item = collection[i];
                        if (item == null) continue;
                        
                        if(CanResolve(item.GetType()))
                            collection[i] = Resolve(item as IAssetReference);
                        else
                            ResolveComponent(item);
                    }
                    
                    field.SetValue(component, collection);
                }
                else
                {
                    if(!CanResolve(value.GetType()))
                        continue;
                    
                    var resolved = Resolve(value as IAssetReference);
                    if (resolved != null)
                        field.SetValue(component, resolved);
                }
            }
        }

        private static IEnumerable<FieldInfo> GetSerializedFields(Type type)
        {
            var ret = new List<FieldInfo>();
            var toCheck = type;
            
            do
            {
                ret.AddRange(toCheck
                    .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(x => x.IsPublic || x.GetCustomAttribute<SerializeField>() != null));
                toCheck = toCheck.BaseType;
            } while (toCheck != typeof(UnityEngine.Object) && toCheck != null);

            return ret;
        }
    }
}