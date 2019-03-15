using System.Collections.Generic;

namespace Models.EventModels
{
    public class PlayerJoinedEvent
    {
        public string PlayerId { get; set; }
        public string Name { get; set; }
    }
}