using UnityEngine;

namespace Overmine.API.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject Resolve(this GameObject obj)
        {
            var components = obj.GetComponents<Component>();
            foreach(var component in components)
                Assets.Assets.ResolveComponent(component);
            return obj;
        }
    }
}