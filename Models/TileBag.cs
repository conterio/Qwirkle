using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class TileBag
    {
        public TileBag(int numOfSameShapeAndColor = 3)
        {
            Tiles = new List<Tile>();

            //Fill bag with tiles
            for (int i = 0; i < numOfSameShapeAndColor; ++i)
            {
                foreach (Color color in Enum.GetValues(typeof(Color)))
                {
                    foreach (Shape shape in Enum.GetValues(typeof(Shape)))
                    {
                        Tiles.Add(new Tile(color, shape));
                    }
                }
            }

            Shuffle();
        }
        private List<Tile> Tiles;

        public List<Tile> DrawTiles(int numberOfTiles)
        {
            var drawnTiles = new List<Tile>();
            for(int i = 0; i < numberOfTiles; ++i)
            {
                if(Tiles.Any())
                {
                    drawnTiles.Add(Tiles[0]);
                    Tiles.RemoveAt(0);
                }
            }
            return drawnTiles;
        }

        public List<Tile> ReturnTiles(List<Tile> tiles)
        {
            //Check if there are enough tiles in the bag
            if(tiles.Count() > Tiles.Count())
            {
                //There aren't enough tiles this is an invalid move
                throw new Exception("There aren't enough tiles");
            }

            //Set aside the tiles you want to trade and draw replacement tiles
            var drawnTiles = new List<Tile>();
            for (int i = 0; i < tiles.Count(); ++i)
            {
                if (Tiles.Any())
                {
                    drawnTiles.Add(Tiles[0]);
                    Tiles.RemoveAt(0);
                }
            }

            //mix the tiles that you traded away back into the bag
            foreach (var tile in tiles)
            {
                Tiles.Add(tile);
            }
            Shuffle();

            return drawnTiles;
        }

        public void Shuffle()
        {
            var rng = new Random();
            Tiles = Tiles.OrderBy(_ => rng.Next()).ToList();
        }
    }
}