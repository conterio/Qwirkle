using System.Collections.Generic;

namespace Models.EventModels
{
    public class StartGameEvent
    {
        public string CurrentPlayerId { get; set; }
        public List<string> PlayerOrder { get; set; }
    }
}