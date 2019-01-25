using System;

namespace Models
{
    public class GameSettings
    {
        private const int DefaultHandSize = 6;
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public int HumanTimeout { get; set; }
        public int AITimeout { get; set; }
        public int NumberOfTiles { get; set; }
        public int HandSize { get; set; } = DefaultHandSize;

        /*
         * max players?
         *
         */
    }
}