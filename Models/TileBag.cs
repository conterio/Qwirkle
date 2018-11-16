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
            Tiles = (from _ in Enumerable.Repeat(0, numOfSameShapeAndColor)
                     from color in Enum.GetValues(typeof(Color)).Cast<Color>()
                     from shape in Enum.GetValues(typeof(Shape)).Cast<Shape>()
                     select new Tile(color, shape)).ToList();

            Shuffle();
        }

        private List<Tile> Tiles;

        public List<Tile> DrawTiles(int numberOfTiles)
        {
            var drawnTiles = Tiles.Take(numberOfTiles).ToList();
            Tiles.RemoveRange(0, drawnTiles.Count);
            return drawnTiles;
        }

        public List<Tile> ReturnTiles(List<Tile> returnedTiles)
        {
            //Check if there are enough tiles in the bag
            if (returnedTiles.Count > Tiles.Count)
            {
                //There aren't enough tiles this is an invalid move
                throw new Exception("There aren't enough tiles");
            }

            //Set aside the tiles you want to trade and draw replacement tiles
            var drawnTiles = DrawTiles(returnedTiles.Count);

            //mix the tiles that you traded away back into the bag
            Tiles.AddRange(returnedTiles);
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