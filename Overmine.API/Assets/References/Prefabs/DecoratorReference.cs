using Thor;
using UnityEngine;

namespace Overmine.API.Assets.References
{
    public class DecoratorReference : Decorator, INamedReference
    {
        [SerializeField]
        private string m_name;
        
        public string Name => m_name;
        
        public Object Resolve(GameObject obj)
        {
            return obj.GetComponent<Decorator>();
        }
    }
}