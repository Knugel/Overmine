using System.Collections.Generic;
using System.Linq;
using Overmine.API.Assets.References;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets.Resolvers
{
    public class PopupResolver : AssetResolver<Popup, PopupReference>
    {
        private static readonly Dictionary<string, Popup> Popups = new Dictionary<string, Popup>();
        
        protected override Popup ResolveInternal(PopupReference reference)
        {
            if(Popups.Count == 0)
                GetPopups();
            var resolved = Popups.TryGetValue(reference.Guid, out var value) ? value : null;
            Debug.Log("Resolving reference " + reference.Guid + " to " + resolved);
            return resolved;
        }

        private static void GetPopups()
        {
            var resources = GameData.Instance.Popups
                .Where(x => !(x is IAssetReference));
            foreach (var resource in resources)
            {
                if (Popups.ContainsKey(resource.Guid))
                    continue;
                Popups.Add(resource.Guid, resource);
            }
        }
    }
}