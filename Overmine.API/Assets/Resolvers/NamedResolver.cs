using System.Linq;
using Overmine.API.Assets.References;
using UnityEngine;

namespace Overmine.API.Assets.Resolvers
{
    public class NamedResolver : AssetResolver<Object, INamedReference>
    {
        protected override Object ResolveInternal(INamedReference reference)
        {
            var objects = Resources.FindObjectsOfTypeAll<GameObject>();
            var go = objects.FirstOrDefault(x => x.name == reference.Name);
            return go != null ? reference.Resolve(go) : null;
        }
    }
}