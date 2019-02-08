    using System.Collections.Generic;

namespace Models.ViewModels
{
    public class PlayerViewModel
    {
        public int Score { get; set; }
        public string Name { get; set; }
        public bool IsHumanPlayer { get; set; }
        public List<Tile> CurrentHand { get; set; }
		public bool StillPlaying { get; set; }
	}
}