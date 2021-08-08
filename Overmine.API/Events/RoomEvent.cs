using Thor;

namespace Overmine.API.Events
{
    public abstract class RoomEvent
    {
        public abstract class Setup : RoomEvent
        {
            public readonly Room Room;

            protected Setup(Room room)
            {
                Room = room;
            }
            
            public class Pre : Setup
            {
                public bool IsCanceled = false;

                public Pre(Room room): base(room) {}
            }

            public class Post : Setup
            {
                public Post(Room room) : base(room) { }
            }
        }
    }
}