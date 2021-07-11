using Thor;
using UnityEngine;

namespace Overmine.API.Events
{
    public abstract class PopupEvent
    {
        public abstract class Create : PopupEvent
        {
            public readonly Popup Prefab;
            public readonly object Data;
            public readonly Entity Owner;
            public readonly Vector3 Position;
            public readonly Entity Follow;
            
            public Popup Result;
            
            public Create(Popup prefab, object data, Entity owner, Vector3 position, Entity follow)
            {
                Prefab = prefab;
                Data = data;
                Owner = owner;
                Position = position;
                Follow = follow;
                
                Result = null;
            }
            
            public class Pre : Create
            {
                public bool IsCanceled = false;

                public Pre(Popup prefab, object data, Entity owner, Vector3 position, Entity follow) : base(prefab,
                    data, owner, position, follow) { }
            }

            public class Post : Create
            {
                public Post(Popup prefab, object data, Entity owner, Vector3 position, Entity follow,
                    Popup result) : base(prefab, data, owner, position, follow)
                {
                    Result = result;
                }
            }
        }
    }
}