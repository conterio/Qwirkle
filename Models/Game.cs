using System;
using Models.Enums;

namespace Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public ITurn[] Turns { get; set; }
        public GameSettings GameSettings { get; set; }
        public GameStatus Status { get; set; }
    }
}
