using System;

namespace Models
{
    public class GameSettings
    {
        private const int DefaultHandSize = 6;
		private const int DefaultMaxPlayers = 4;
        public string Name { get; set; }
        public int HumanTimeout { get; set; }
        public int AITimeout { get; set; }
        public int NumberOfTiles { get; set; }
        public int HandSize { get; set; } = DefaultHandSize;
		public int MaxPlayers { get; set; } = DefaultMaxPlayers;

	}
}