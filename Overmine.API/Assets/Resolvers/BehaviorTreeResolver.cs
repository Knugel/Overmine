using System.Linq;
using Overmine.API.Assets.References;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets.Resolvers
{
    public class BehaviorTreeResolver : AssetResolver<ExtendedExternalBehaviorTree, BehaviorTreeReference>
    {
        protected override ExtendedExternalBehaviorTree ResolveInternal(BehaviorTreeReference reference)
        {
            var trees = Resources.FindObjectsOfTypeAll<ExtendedExternalBehaviorTree>();
            return trees.FirstOrDefault(x => x.BehaviorSource.behaviorName == reference.BehaviorSource.behaviorName);
        }
    }
}