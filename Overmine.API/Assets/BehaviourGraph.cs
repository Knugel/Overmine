using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets
{
    public class BehaviourGraph : ExtendedExternalBehaviorTree
    {
        [NonSerialized]
        public List<NodeLink> Links = new List<NodeLink>();
        
        [NonSerialized]
        public List<NodeData> NodesData = new List<NodeData>();

        [HideInInspector]
        public List<Variable> Variables = new List<Variable>();

        public IEnumerable<NodeData> GetLinked(NodeData node)
        {
            var links = Links.Where(x => x.SourceGuid == node.GUID);
            return NodesData
                .Where(x => links.Any(y => y.TargetGuid == x.GUID))
                .OrderBy(x => x.Position.x);
        }
    }

    [Serializable]
    public class NodeData : ISerializationCallbackReceiver
    {
        public string GUID;

        public Vector2 Position;
        
        [NonSerialized]
        public Type Type;
        
        [SerializeReference]
        public Task Data;

        [SerializeField]
        private string _type;

        public void OnBeforeSerialize()
        {
            if (Data == null || Type == null)
                return;
            _type = Type?.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            Type = Type.GetType(_type);
        }
    }

    [Serializable]
    public class NodeLink
    {
        public string SourceGuid;
        
        public string TargetGuid;
    }

    [Serializable]
    public class Variable : ISerializationCallbackReceiver
    {
        public string Name;

        [NonSerialized]
        public Type Type;

        public bool IsGlobal;
        
        [SerializeField]
        private string _type;
        
        public void OnBeforeSerialize()
        {
            _type = Type?.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            Type = Type.GetType(_type);
        }
    }
}