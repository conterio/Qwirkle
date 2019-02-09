using System;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Qwirkle.Models
{
    public class GameBoard
    {
        public GameBoard()
        {
            _tilePlacements = new List<TilePlacement>();
        }

        List<TilePlacement> _tilePlacements;

        /// <summary>
        /// Adds tilesPlacements to the game board if they are valid
        /// </summary>
        /// <param name="tilesPlacements"></param>
        /// <returns>
        /// Returns true if it's a valid move and all tilesPlacements were added to the board
        /// Returns false if it's an invalid move. None of the tilesPlacements will be added to the board
        /// </returns>
        public bool AddTiles(List<TilePlacement> tilesPlacements)
        {
            //You have to place at least one tile
            if (tilesPlacements == null)
                return false;

            var originalTilePlacements = _tilePlacements.Select(tileP => new TilePlacement(tileP)).ToList();
            foreach (var tilesPlacement in tilesPlacements)
            {
                foreach (var direction in (DirectionEnum[])Enum.GetValues(typeof(DirectionEnum)))
                {
                    if(!ValidateTilePlacement(tilesPlacement, direction))
                    {
                        //There was an invalid tilePlacement return the GameBoard to it's original state
                        _tilePlacements = originalTilePlacements.Select(tileP => new TilePlacement(tileP)).ToList();
                        return false;
                    }
                }
                _tilePlacements.Add(tilesPlacement);
            }
            return true;
        }

        private bool ValidateTilePlacement(TilePlacement tilePlacement, DirectionEnum direction)
        {
            var tiles = GetTilesInDirection(tilePlacement, direction);
            if (tiles.Count == 0)
            {
                //Validate it isn't the first turn of the game
                if(_tilePlacements.Exists(tp => tp.XCoord == 0 && tp.YCoord ==0))
                {
                    //There are no tiles touching this tilePlacement but it's not the first tile of the game
                    //This is an invalid move
                    return false;
                }
                //It's the first tilePlacement of the game
                return true;
            }

            if (tiles.Any(t => t.Shape == tilePlacement.Tile.Shape && t.Color == tilePlacement.Tile.Color))
            {
                //If we find any tiles with the same shape and same color the turn is invalid
                return false;
            }

            //Check if we are validating against Shape or Color
            if (tiles.Any(t => t.Color != tiles[0].Color))
            {
                //Validate same shape different color
                //If we find any tiles with a different shape the turn is invalid
                return tiles.Any(t => t.Shape != tilePlacement.Tile.Shape);
            }
            else
            {
                //Or validate same color different shape
                //If we find any tiles with a different color the turn is invalid
                return tiles.Any(t => t.Color != tilePlacement.Tile.Color);
            }
        }

        private List<Tile> GetTilesInDirection(TilePlacement tilePlacement, DirectionEnum direction)
        {
            var tiles = new List<Tile>();
            var xCoordIndex = tilePlacement.XCoord;
            var yCoordIndex = tilePlacement.YCoord;
            var hitEmptyTilePlacement = false;
            switch (direction)
            {
                case DirectionEnum.up:
                    do
                    {
                        var uPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == ++yCoordIndex);
                        if (uPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(uPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                case DirectionEnum.right:
                    do
                    {
                        var rPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == ++xCoordIndex && tp.YCoord == tilePlacement.YCoord);
                        if (rPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(rPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                case DirectionEnum.down:
                    do
                    {
                        var dPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == --yCoordIndex);
                        if (dPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(dPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                case DirectionEnum.left:
                    do
                    {
                        var lPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == --xCoordIndex && tp.YCoord == tilePlacement.YCoord);
                        if (lPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(lPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return tiles;
        }

        private enum DirectionEnum
        {
            up = 1,
            right = 2,
            down = 3,
            left = 4
        }
    }
}