using System;

namespace Models
{
    public class Player
    {
        private const int MAX_HAND_SIZE = 6;

        public Player()
        {
            this.CurrentHand = new Tile[MAX_HAND_SIZE];   
        }
        public string Name { get; set; }
        public int Score { get; set; }
        public Tile[] CurrentHand { get; set; }
    }
}