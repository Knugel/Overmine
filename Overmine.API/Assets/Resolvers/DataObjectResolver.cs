using System.Collections.Generic;
using System.Linq;
using Overmine.API.Assets.References;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets.Resolvers
{
    public class DataObjectResolver : AssetResolver<DataObject, IDataObjectReference>
    {
        private static readonly Dictionary<string, DataObject> DataObjects = new Dictionary<string, DataObject>();

        protected override DataObject ResolveInternal(IDataObjectReference reference)
        {
            if(DataObjects.Count == 0)
                GetDataObjects();
            return DataObjects.TryGetValue(reference.Guid, out var value) ? value : null;
        }

        private static void GetDataObjects()
        {
            var resources = Resources
                .FindObjectsOfTypeAll<DataObject>()
                .Where(x => !(x is IAssetReference));
            foreach (var resource in resources)
            {
                if (DataObjects.ContainsKey(resource.guid))
                    continue;
                DataObjects.Add(resource.guid, resource);
            }
        }
    }
}