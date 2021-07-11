using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Overmine.API
{
    public class ModResources
    {
        public IReadOnlyCollection<AssetBundle> Bundles { get; }

        public ModResources(IEnumerable<AssetBundle> bundles)
        {
            Bundles = bundles.ToList();
        }

        public AssetBundle GetBundleForResource(string name)
        {
            return Bundles.FirstOrDefault(x => x.Contains(name));
        }

        public T LoadAsset<T>(string name) where T : Object
        {
            return Bundles.Select(x => x.LoadAsset<T>(name)).FirstOrDefault(x => x != null);
        }

        public bool Contains(string name)
        {
            return Bundles.Any(x => x.Contains(name));
        }

        public IEnumerable<T> LoadAllAssets<T>() where T : Object
        {
            return Bundles.SelectMany(x => x.LoadAllAssets<T>());
        }

        public IEnumerable<string> GetAllAssetNames()
        {
            return Bundles.SelectMany(x => x.GetAllAssetNames());
        }

        public IEnumerable<T> LoadAllObjectsWithComponent<T>()
        {
            return LoadAllAssets<GameObject>()
                .Select(x => x.GetComponent<T>())
                .Where(x => x != null)
                .ToList();
        }
    }
}