using System.Collections.Generic;
using System.Linq;
using Overmine.API.Assets.References;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets.Resolvers
{
    public class EntityResolver : AssetResolver<Entity, EntityReference>
    {
        private static readonly Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();

        protected override Entity ResolveInternal(EntityReference reference)
        {
            if(Entities.Count == 0)
                GetEntities();
            return Entities.TryGetValue(reference.Guid, out var value) ? value : null;
        }
        
        private static void GetEntities()
        {
            var entities = Resources
                .FindObjectsOfTypeAll<GameObject>()
                .Select(x => x.GetComponent<Entity>())
                .Where(x => x != null && !(x is EntityReference));
            foreach (var entity in entities)
            {
                if (Entities.ContainsKey(entity.Guid))
                    continue;
                Entities.Add(entity.Guid, entity);
            }
        }
    }
}