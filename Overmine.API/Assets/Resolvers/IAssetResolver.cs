using System;
using Overmine.API.Assets.References;
using Object = UnityEngine.Object;

namespace Overmine.API.Assets.Resolvers
{
    public interface IAssetResolver
    {
        Object Resolve(IAssetReference reference);

        bool CanResolve(Type type);
    }

    public abstract class AssetResolver<T, U> : IAssetResolver where U : class, IAssetReference where T : Object
    {
        public Object Resolve(IAssetReference reference)
        {
            return ResolveInternal(reference as U);
        }

        public virtual bool CanResolve(Type type)
        {
            return typeof(U).IsAssignableFrom(type);
        }

        protected abstract T ResolveInternal(U reference);
    }
}