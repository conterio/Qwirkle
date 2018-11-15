using System;

namespace Models
{
    public class Tile : IEquatable<Tile>
    {
        public Tile(Color color, Shape shape)
        {
            this.Color = color;
            this.Shape = Shape;
        }

        public Color Color { get; set; }
        public Shape Shape { get; set; }

        public bool Equals(Tile tile)
        {
            if(this.Color == tile.Color && this.Shape == tile.Shape)
            {
                return true;
            }
            return false;
        }
    }
}