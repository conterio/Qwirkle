using System.Collections.Generic;

namespace Models
{
    public class Player
    {
        private const int MAX_HAND_SIZE = 6;

        public Player()
        {
            this.CurrentHand = new List<Tile>();
        }
        public int Score { get; set; }
        public List<Tile> CurrentHand { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public bool IsHumanPlayer { get; set; }
        public bool IsSpectator { get; set; }
		public bool StillPlaying { get; set; }
    }
}