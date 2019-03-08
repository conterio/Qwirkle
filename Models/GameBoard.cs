using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class GameBoard
    {
        public GameBoard()
        {
            _tilePlacements = new List<TilePlacement>();
        }

        //This is a list of all the tiles that have been played on the board so far
        private List<TilePlacement> _tilePlacements;

        /// <summary>
        /// Adds tilesPlacements to the game board if they are valid
        /// </summary>
        /// <param name="tilesPlacements"></param>
        /// <returns>
        /// Returns score if it's a valid move and all tilesPlacements were added to the board
        /// Returns -1 if it's an invalid move. None of the tilesPlacements will be added to the board
        /// </returns>
        public int AddTiles(List<TilePlacement> tilesPlacements)
        {
            var score = 0;
            //You have to place at least one tile
            if (tilesPlacements == null)
                return -1;

            var firstTurn = !_tilePlacements.Any();

            var result = IsScatteredPlacement(tilesPlacements);
            if (result.isScattered)
            {
                return -1;
            }
            var directionOfPlay = result.direction;

            var originalTilePlacements = _tilePlacements.Select(tileP => new TilePlacement(tileP)).ToList();
            foreach (var tilePlacement in tilesPlacements)
            {
                if (firstTurn)
                {
                    if (tilePlacement.XCoord != 0 && tilePlacement.YCoord != 0)
                    {
                        //Fist **smack**
                        //First turn and they are not playing at 0,0
                        return -1;
                    }

                    if (tilesPlacements.Count == 1)
                    {
                        //Only playing one tile and it's at 0,0 return a score of 1
                        return 1;
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
                        return -1;
                    }

                }

                foreach (var direction in (DirectionEnum[])Enum.GetValues(typeof(DirectionEnum)))
                {
                    if (!ValidateTilePlacement(tilePlacement, direction))
                    {
                        //There was an invalid tilePlacement return the GameBoard to it's original state
                        _tilePlacements = originalTilePlacements.Select(tileP => new TilePlacement(tileP)).ToList();
                        return -1;
                    }
                }
                _tilePlacements.Add(tilePlacement);
                //Calculate score for the opposite direction of play
                var tilesInOppositeDirection = GetTilesInDirection(tilePlacement, OppositeDirection(directionOfPlay));
                tilesInOppositeDirection.Add(tilePlacement.Tile);
                score += CalculateScore(tilesInOppositeDirection);
            }

            //Calculate score for the direction of play
            var tilesInDirectionOfPlay = GetTilesInDirection(tilesPlacements.FirstOrDefault(), directionOfPlay);
            tilesInDirectionOfPlay.Add(tilesPlacements.FirstOrDefault()?.Tile);
            score += CalculateScore(tilesInDirectionOfPlay, firstTurn);
            return score;
        }

        private (bool isScattered, DirectionEnum direction) IsScatteredPlacement(List<TilePlacement> tilePlacements)
        {
            if (tilePlacements.Count < 2)
            {
                //Either direction can be used by default single tile placements are horizontal
                //got to pick one. Just got to. Can't be none.
                return (false, DirectionEnum.Horizontal);
            }

            DirectionEnum directionOfPlay;

            //check for vertical or horizontal play
            if (tilePlacements[0].XCoord == tilePlacements[1].XCoord)
            {
                //Vertical play
                directionOfPlay = DirectionEnum.Vertical;
                var xIndex = tilePlacements[0].XCoord;
                //Check if any other tile placements don't share the same XCoord
                if (tilePlacements.Any(t => t.XCoord != xIndex))
                {
                    //Not all tiles share the same XCoord
                    return (true, default(DirectionEnum));
                }

                //Make sure all the tiles we are playing are together without any whitespace separating them
                for (int yIndex = tilePlacements.Min(t => t.YCoord); yIndex < tilePlacements.Max(t => t.YCoord); ++yIndex)
                {
                    if (!tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex) && //Check to see if it's not in the tiles we are going to place
                        !_tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex)) //Check to see if the tile is not on the board
                    {
                        //Invalid move
                        return (true, default(DirectionEnum));
                    }
                }
            }
            else if (tilePlacements[0].YCoord == tilePlacements[1].YCoord)
            {
                //Horizontal play
                directionOfPlay = DirectionEnum.Horizontal;
                //Check if any other tile placements don't share the same YCoord
                var yIndex = tilePlacements[0].YCoord;
                if (tilePlacements.Any(t => t.YCoord != yIndex))
                {
                    //Not all tiles share the same YCoord
                    return (true, default(DirectionEnum));
                }

                //Make sure all the tiles we are playing are together without any whitespace separating them
                for (int xIndex = tilePlacements.Min(t => t.XCoord); xIndex < tilePlacements.Max(t => t.XCoord); ++xIndex)
                {
                    if (!tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex) && //Check to see if it's not in the tiles we are going to place
                        !_tilePlacements.Any(t => t.XCoord == xIndex && t.YCoord == yIndex)) //Check to see if the tile is not on the board
                    {
                        //Invalid move
                        return (true, default(DirectionEnum));
                    }
                }
            }
            else
            {
                //Invalid move
                return (true, default(DirectionEnum));
            }

            return (false, directionOfPlay);
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
                return tiles.All(t => t.Shape == tilePlacement.Tile.Shape);
            }
            else
            {
                //Or validate same color different shape
                //If we find any tiles with a different color the turn is invalid
                return tiles.All(t => t.Color == tilePlacement.Tile.Color);
            }
        }

        private List<Tile> GetTilesInDirection(TilePlacement tilePlacement, DirectionEnum direction)
        {
            var tiles = new List<Tile>();
            var hitEmptyTilePlacement = false;

            switch (direction)
            {
                case DirectionEnum.Horizontal:
                    var xCoordIndex = tilePlacement.XCoord;
                    //Get tiles to the left
                    do
                    {
                        --xCoordIndex;
                        var lPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == xCoordIndex && tp.YCoord == tilePlacement.YCoord);
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
                        ++xCoordIndex;
                        var rPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == xCoordIndex && tp.YCoord == tilePlacement.YCoord);
                        if (rPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(rPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
                case DirectionEnum.Vertical:
                    var yCoordIndex = tilePlacement.YCoord;
                    //Get tiles above
                    do
                    {
                        ++yCoordIndex;
                        var uPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == yCoordIndex);
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
                        --yCoordIndex;
                        var dPlacement = _tilePlacements.SingleOrDefault(tp => tp.XCoord == tilePlacement.XCoord && tp.YCoord == yCoordIndex);
                        if (dPlacement == null)
                            hitEmptyTilePlacement = true;
                        else
                            tiles.Add(dPlacement.Tile);
                    } while (hitEmptyTilePlacement == false);
                    break;
            }

            return tiles;
        }

        private enum DirectionEnum
        {
            None = 0,
            Horizontal = 1,
            Vertical = 2
        }

        private int CalculateScore(List<Tile> tiles, bool firstTurn = false)
        {
            if (tiles.Count == 6)
                //Qwirkle!!!!! **fist** smack
                return 12;
            else if (tiles.Count == 1)
                if (firstTurn)
                    return 1;
                else
                    return 0;
            else
                return tiles.Count;
        }

        private DirectionEnum OppositeDirection(DirectionEnum direction)
        {
            if (direction == DirectionEnum.Horizontal)
            {
                return DirectionEnum.Vertical;
            }
            else
            {
                return DirectionEnum.Horizontal;
            }
        }
    }
}