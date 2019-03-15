using System.Collections.Generic;

namespace Models.EventModels
{
    public class GameInfoEvent
    {
        public List<(string PlayerId, string Name)> Players { get; set; }
        public GameSettings GameSettings { get; set; }
    }
}