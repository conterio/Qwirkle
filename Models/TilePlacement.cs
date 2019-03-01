using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class TilePlacement
    {
        public TilePlacement()
        {

        }
        public TilePlacement(TilePlacement tilePlacement)
        {
            Tile = tilePlacement.Tile;
            XCoord = tilePlacement.XCoord;
            YCoord = tilePlacement.YCoord;
        }

        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public Tile Tile { get; set; }

        public (int x, int y) Above => (XCoord, YCoord + 1);
        public (int x, int y) Below => (XCoord, YCoord - 1);
        public (int x, int y) Left => (XCoord - 1, YCoord);
        public (int x, int y) Right => (XCoord + 1, YCoord);
        public IEnumerable<(int x, int y)> Neighbors => new[] { Above, Below, Left, Right };

        public bool IsNeighbor(int x, int y) =>
            Neighbors.Any(placement => placement.Equals((x, y)));
    }
}