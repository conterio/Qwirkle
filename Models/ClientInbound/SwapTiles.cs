using System.Collections.Generic;

namespace Models
{
    public class SwapTiles : ITurn
    {
        public List<Tile> TurnedInTiles { get; set; }
        public List<Tile> NewTiles { get; set; }
    }
}