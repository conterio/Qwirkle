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

            var firstTurn = !_tilePlacements.Any();

            if (CheckForScatteredPlacement(tilesPlacements))
            {
                return false;
            }

            var originalTilePlacements = _tilePlacements.Select(tileP => new TilePlacement(tileP)).ToList();
            foreach (var tilePlacement in tilesPlacements)
            {
                if (firstTurn)
                {
                    if (tilePlacement.XCoord != 0 && tilePlacement.YCoord != 0)
                    {
                        //Fist **smack** turn and they are not playing at 0,0
                        return false;
                    }
                    firstTurn = false;
                }
                else
                {
                    //Make sure it has a neighbor if it's not the first turn of the game
                    bool hasNeighbors = CheckForNeighbors(_tilePlacements, tilePlacement.XCoord, tilePlacement.YCoord);
                    if (!hasNeighbors)
                    {
                        //There were no neighbors, so this is an invalid move
                        return false;
                    }

                }

                foreach (var direction in (DirectionEnum[])Enum.GetValues(typeof(DirectionEnum)))
                {
                    if (!ValidateTilePlacement(tilePlacement, direction))
                    {
                        //There was an invalid tilePlacement return the GameBoard to it's original state
                        _tilePlacements = originalTilePlacements.Select(tileP => new TilePlacement(tileP)).ToList();
                        return false;
                    }
                }
                _tilePlacements.Add(tilePlacement);
            }
            return true;
        }

        private bool CheckForScatteredPlacement(List<TilePlacement> tilePlacements)
        {
            if (tilePlacements.Count < 2)
            {
                return false;
            }

            //check for vertical or horizontal play
            if (tilePlacements[0].XCoord == tilePlacements[1].XCoord)
            {
                //Vertical play
                var xIndex = tilePlacements[0].XCoord;
                //Check if any other tile placements don't share the same XCoord
                if (tilePlacements.Any(t => t.XCoord != xIndex))
                {
                    //Not all tiles share the same XCoord
                    return true;
                }

                //Make sure all the tiles we are playing are together without any whitespace separating them
                for (int yIndex = tilePlacements.Min(t => t.YCoord); yIndex < tilePlacements.Max(t => t.YCoord); ++yIndex)
                {
                    if (!tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex) && //Check to see if it's not in the tiles we are going to place
                        !_tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex)) //Check to see if the tile is not on the board
                    {
                        //Invalid move
                        return true;
                    }
                }
            }
            else if (tilePlacements[0].YCoord == tilePlacements[1].YCoord)
            {
                //Horizontal play
                //Check if any other tile placements don't share the same YCoord
                var yIndex = tilePlacements[0].YCoord;
                if (tilePlacements.Any(t => t.YCoord != yIndex))
                {
                    //Not all tiles share the same YCoord
                    return true;
                }

                //Make sure all the tiles we are playing are together without any whitespace separating them
                for (int xIndex = tilePlacements.Min(t => t.XCoord); xIndex < tilePlacements.Max(t => t.XCoord); ++xIndex)
                {
                    if (!tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex) && //Check to see if it's not in the tiles we are going to place
                        !_tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex)) //Check to see if the tile is not on the board
                    {
                        //Invalid move
                        return true;
                    }
                }
            }
            else
            {
                //Invalid move
                return true;
            }

            return false;
        }

        private bool CheckForNeighbors(List<TilePlacement> tilePlacements, int xCoord, int yCoord)
        {
            return tilePlacements.Any(placement => placement.IsNeighbor(xCoord, yCoord));
        }

        private bool ValidateTilePlacement(TilePlacement tilePlacement, DirectionEnum direction)
        {

            //check to see if they are trying to place a tile on top of a tile that already exists
            if (_tilePlacements.Any(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == tilePlacement.YCoord))
            {
                //There is already a tile at this location
                return false;
            }

            var tiles = GetTilesInDirection(tilePlacement, direction);
            if (tiles.Count == 0)
            {
                //This direction is valid because there are no tiles
                return true;
            }

            if (tiles.Any(t => t.Shape == tilePlacement.Tile.Shape && t.Color == tilePlacement.Tile.Color))
            {
                //If we find any tiles with the same shape and same color the turn is invalid
                return false;
            }

            if (tiles.Count == 1)
            {
                return tiles[0].Color == tilePlacement.Tile.Color || tiles[0].Shape == tilePlacement.Tile.Shape;
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
            var hitEmptyTilePlacement = false;

            switch (direction)
            {
                case DirectionEnum.horizontal:
                    var xCoordIndex = tilePlacement.XCoord;
                    //Get tiles to the left
                    do
                    {
                        var lPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == --xCoordIndex && tp.YCoord == tilePlacement.YCoord);
                        if (lPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(lPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);

                    //Get tiles to the right
                    xCoordIndex = tilePlacement.XCoord;
                    hitEmptyTilePlacement = false;
                    do
                    {
                        var rPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == ++xCoordIndex && tp.YCoord == tilePlacement.YCoord);
                        if (rPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(rPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                case DirectionEnum.vertical:
                    var yCoordIndex = tilePlacement.YCoord;
                    //Get tiles above
                    do
                    {
                        var uPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == ++yCoordIndex);
                        if (uPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(uPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);

                    //Get tiles below
                    yCoordIndex = tilePlacement.YCoord;
                    hitEmptyTilePlacement = false;
                    do
                    {
                        var dPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == --yCoordIndex);
                        if (dPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(dPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return tiles;
        }

        private enum DirectionEnum
        {
            horizontal = 1,
            vertical = 2
        }
    }
}