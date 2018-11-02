namespace Models
{
    public class SwapTiles : ITurn
    {
        public Tile[] TurnedInTiles { get; set; }
        public Tile[] NewTiles { get; set; }
    }
}