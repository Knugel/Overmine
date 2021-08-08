using Thor;

namespace Overmine.API.Events
{
    public abstract class SetupEvent
    {
        public readonly GameData Instance;

        protected SetupEvent(GameData instance)
        {
            Instance = instance;
        }

        public class Pre : SetupEvent
        {
            public Pre(GameData instance): base(instance) { }
        }
        
        public class Post : SetupEvent
        {
            public Post(GameData instance): base(instance) { }
        }
    }
}