namespace Models
{
    public class PlaceTiles : ITurn {
        public TilePlacement[] Placements;
        public Tile[] NewTiles { get; set; }
    }
}