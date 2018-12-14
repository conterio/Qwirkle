using System;

namespace Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public ITurn[] Turns { get; set; }
    }
}
