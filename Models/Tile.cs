using System;

namespace Models
{
    public class Tile : IEquatable<Tile>
    {
        public Tile(Color color, Shape shape)
        {
            this.Color = color;
            this.Shape = shape;
        }

        public Color Color { get; }
        public Shape Shape { get; }

		public override string ToString()
		{
			return this.Color + ":" + this.Shape;
		}

		public bool Equals(Tile tile)
        {
			if(tile == null)
			{
				return false;
			}

            if(this.Color == tile.Color && this.Shape == tile.Shape)
            {
                return true;
            }
            return false;
        }

		public override bool Equals(object obj)
		{
			return Equals(obj as Tile);
		}

		public override int GetHashCode()
		{
			return Color.GetHashCode() ^ Shape.GetHashCode();
		}
	}
}