using Thor;

namespace Overmine.API.Events
{
    public readonly struct SetupEvent
    {
        public readonly struct Pre
        {
            public readonly GameData Instance;
            
            public Pre(GameData instance)
            {
                Instance = instance;
            }
        }
        
        public readonly struct Post
        {
            public readonly GameData Instance;
            
            public Post(GameData instance)
            {
                Instance = instance;
            }
        }
    }
}