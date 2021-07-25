using UnityEngine;

namespace Overmine.API.Assets.References
{
    public interface INamedReference : IAssetReference
    {
        string Name { get; }

        Object Resolve(GameObject obj);
    }
}