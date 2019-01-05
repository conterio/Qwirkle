using System;

namespace Models
{
    public class GameSettings
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public int HumanTimeout { get; set; }
        public int AITimeout { get; set; }
        public int NumberOfTiles { get; set; }

        /*
         * max players?
         *
         */
    }
}