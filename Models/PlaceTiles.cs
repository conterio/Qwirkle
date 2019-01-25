using System.Collections.Generic;

namespace Models
{
    public class PlaceTiles : ITurn {
        public List<TilePlacement> Placements;
        public List<Tile> NewTiles { get; set; }
    }
}