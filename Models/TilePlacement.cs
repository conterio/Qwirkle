namespace Models
{
    public class TilePlacement
    {
        public TilePlacement(TilePlacement tilePlacement)
        {
            Tile = tilePlacement.Tile;
            XCoord = tilePlacement.XCoord;
            YCoord = tilePlacement.YCoord;
        }

        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public Tile Tile { get; set; }
    }
}