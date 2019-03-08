using System.Collections.Generic;

namespace Models.EventModels
{
    public class TurnEvent
	{
        public string CurrentPlayerId { get; set; }
        public string PreviousPlayerId { get; set; }
        public List<TilePlacement> TilesPlayed { get; set; }
        public int Score { get; set; }
        public bool RemovePlayer { get; set; }
        public int SwappedTiles { get; set; }
	}
}