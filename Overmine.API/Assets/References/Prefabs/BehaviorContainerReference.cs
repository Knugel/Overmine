using Thor;
using UnityEngine;

namespace Overmine.API.Assets.References
{
    public class BehaviorContainerReference : BehaviorContainer, INamedReference
    {
        public string Name => name;
        
        public Object Resolve(GameObject obj)
        {
            return obj.GetComponent<BehaviorContainer>();
        }
    }
}