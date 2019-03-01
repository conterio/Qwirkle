﻿using System;
using System.Collections.Generic;
using System.Text;
using Models;
using NUnit.Framework;
using Qwirkle.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class GameBoardTests
    {
        [Test]
        public void NoTilesPlaced_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();

            //Act
            var tilesAdded = gameBoard.AddTiles(null);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void FirstTurnNot00_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 4, XCoord = 5, Tile = new Tile(Color.blue, Shape.circle)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void ScatteredTilesDifferentXAndY_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 1, XCoord = 1, Tile = new Tile(Color.blue, Shape.square)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void ScatteredTilesDifferentX_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 1, XCoord = 0, Tile = new Tile(Color.blue, Shape.square)},
                new TilePlacement {YCoord = 2, XCoord = 0, Tile = new Tile(Color.blue, Shape.clover)},
                new TilePlacement {YCoord = 2, XCoord = 1, Tile = new Tile(Color.blue, Shape.diamond)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void ScatteredTilesDifferentY_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 1, Tile = new Tile(Color.blue, Shape.square)},
                new TilePlacement {YCoord = 0, XCoord = 2, Tile = new Tile(Color.blue, Shape.clover)},
                new TilePlacement {YCoord = 1, XCoord = 2, Tile = new Tile(Color.blue, Shape.diamond)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void PlaceTileOnAnotherTile_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 1, Tile = new Tile(Color.blue, Shape.square)},
            };
            var tilesAdded = gameBoard.AddTiles(tilePlacements);
            Assert.IsTrue(tilesAdded);

            //Act
            tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void DifferentShapeDifferentColor_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.green, Shape.square)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void SameShapeDifferentColor_GameBoard_ValidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 1, Tile = new Tile(Color.green, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 2, Tile = new Tile(Color.orange, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 3, Tile = new Tile(Color.purple, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 4, Tile = new Tile(Color.red, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 5, Tile = new Tile(Color.yellow, Shape.circle)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsTrue(tilesAdded);
        }

        [Test]
        public void DifferentShapeSameColor_GameBoard_ValidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.red, Shape.circle)},
                new TilePlacement {YCoord = 1, XCoord = 0, Tile = new Tile(Color.red, Shape.square)},
                new TilePlacement {YCoord = 2, XCoord = 0, Tile = new Tile(Color.red, Shape.clover)},
                new TilePlacement {YCoord = 3, XCoord = 0, Tile = new Tile(Color.red, Shape.cross)},
                new TilePlacement {YCoord = 4, XCoord = 0, Tile = new Tile(Color.red, Shape.diamond)},
                new TilePlacement {YCoord = 5, XCoord = 0, Tile = new Tile(Color.red, Shape.star)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsTrue(tilesAdded);
        }

        [Test]
        public void ConnectTwoColumns_GameBoard_ValidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            //Go Up
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.red, Shape.circle)},
                new TilePlacement {YCoord = 1, XCoord = 0, Tile = new Tile(Color.red, Shape.square)},
                new TilePlacement {YCoord = 2, XCoord = 0, Tile = new Tile(Color.red, Shape.clover)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Right
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 2, XCoord = 1, Tile = new Tile(Color.blue, Shape.clover)},
                new TilePlacement {YCoord = 2, XCoord = 2, Tile = new Tile(Color.green, Shape.clover)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Up again
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 3, XCoord = 2, Tile = new Tile(Color.green, Shape.square)},
                new TilePlacement {YCoord = 4, XCoord = 2, Tile = new Tile(Color.green, Shape.diamond)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Left
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 4, XCoord = 1, Tile = new Tile(Color.purple, Shape.diamond)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 4, XCoord = 0, Tile = new Tile(Color.red, Shape.diamond)},
                new TilePlacement {YCoord = 5, XCoord = 0, Tile = new Tile(Color.red, Shape.star)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 3, XCoord = 0, Tile = new Tile(Color.red, Shape.cross)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsTrue(tilesAdded);
        }

        [Test]
        public void ScatteredSameColumn_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            //Go Up
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.red, Shape.circle)},
                new TilePlacement {YCoord = 1, XCoord = 0, Tile = new Tile(Color.red, Shape.square)},
                new TilePlacement {YCoord = 2, XCoord = 0, Tile = new Tile(Color.red, Shape.clover)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Right
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 2, XCoord = 1, Tile = new Tile(Color.blue, Shape.clover)},
                new TilePlacement {YCoord = 2, XCoord = 2, Tile = new Tile(Color.green, Shape.clover)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Up again
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 3, XCoord = 2, Tile = new Tile(Color.green, Shape.square)},
                new TilePlacement {YCoord = 4, XCoord = 2, Tile = new Tile(Color.green, Shape.diamond)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Left
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 4, XCoord = 1, Tile = new Tile(Color.purple, Shape.diamond)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Up
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 4, XCoord = 0, Tile = new Tile(Color.red, Shape.diamond)},
                new TilePlacement {YCoord = 5, XCoord = 0, Tile = new Tile(Color.red, Shape.star)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Scattered Placement in Same Column
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = -1, XCoord = 0, Tile = new Tile(Color.red, Shape.cross)},
                new TilePlacement {YCoord = 6, XCoord = 0, Tile = new Tile(Color.red, Shape.circle)}
            };
            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void ScatteredSameRow_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            //Go Right
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.red, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 1, Tile = new Tile(Color.red, Shape.square)},
                new TilePlacement {YCoord = 0, XCoord = 2, Tile = new Tile(Color.red, Shape.clover)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Up
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 1, XCoord = 2, Tile = new Tile(Color.blue, Shape.clover)},
                new TilePlacement {YCoord = 2, XCoord = 2, Tile = new Tile(Color.green, Shape.clover)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Right again
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 2, XCoord = 3, Tile = new Tile(Color.green, Shape.square)},
                new TilePlacement {YCoord = 2, XCoord = 4, Tile = new Tile(Color.green, Shape.diamond)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Down
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 1, XCoord = 4, Tile = new Tile(Color.purple, Shape.diamond)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Go Right
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 4, Tile = new Tile(Color.red, Shape.diamond)},
                new TilePlacement {YCoord = 0, XCoord = 5, Tile = new Tile(Color.red, Shape.star)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            //Scattered Placement in Same Row
            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = -1, Tile = new Tile(Color.red, Shape.cross)},
                new TilePlacement {YCoord = 0, XCoord = 6, Tile = new Tile(Color.red, Shape.circle)}
            };
            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }

        [Test]
        public void AddToCompletedQwirkle_GameBoard_InvalidMove()
        {
            //Arrange
            var gameBoard = new GameBoard();
            var tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = 0, Tile = new Tile(Color.blue, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 1, Tile = new Tile(Color.green, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 2, Tile = new Tile(Color.orange, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 3, Tile = new Tile(Color.purple, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 4, Tile = new Tile(Color.red, Shape.circle)},
                new TilePlacement {YCoord = 0, XCoord = 5, Tile = new Tile(Color.yellow, Shape.circle)}
            };
            Assert.IsTrue(gameBoard.AddTiles(tilePlacements));

            tilePlacements = new List<TilePlacement>
            {
                new TilePlacement {YCoord = 0, XCoord = -1, Tile = new Tile(Color.green, Shape.circle)}
            };

            //Act
            var tilesAdded = gameBoard.AddTiles(tilePlacements);

            //Assert
            Assert.IsFalse(tilesAdded);
        }
    }
}
