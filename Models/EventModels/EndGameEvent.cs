using System.Collections.Generic;

namespace Models.EventModels
{
    public class EndGameEvent
    {
        public List<(string name, int score)> Scores { get; set; }
    }
}