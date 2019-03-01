using Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests.Models
{
	[TestFixture]
    public class TileBagTests
    {
		#region TileBag Constructor
		[Test]
		public void TileBag_Constructor_PopulatesWithTiles_Defaults3Copies()
		{
			// Act
			var tileBag = new TileBag();
			var tiles = tileBag.Tiles;

			// Assert
			Assert.AreEqual(108, tiles.Count);
			var tileTypes = tiles.GroupBy(x => new { x.Shape, x.Color });

			Assert.AreEqual(36, tileTypes.Distinct().Count());
			Assert.IsTrue(tileTypes.All(x => x.Count() == 3));
		}

		[Test]
		public void TileBag_Constructor_PopulatesWithTiles_CustomNumberOfCopies()
		{
			// Arrange
			var copies = 6;

			// Act
			var tileBag = new TileBag(copies);
			var tiles = tileBag.Tiles;

			// Assert
			Assert.AreEqual(copies * 36, tiles.Count);
			var tileTypes = tiles.GroupBy(x => new { x.Shape, x.Color });

			Assert.AreEqual(36, tileTypes.Distinct().Count());
			Assert.IsTrue(tileTypes.All(x => x.Count() == copies));
		}

		[Test]
		public void TileBag_Constructor_ShufflesTiles()
		{
			// Act
			var tileBag = new TileBag();
			var tileBag2 = new TileBag();

			var tiles = tileBag.Tiles;
			var tiles2 = tileBag2.Tiles;

			// Assert
			Assert.IsFalse(tiles.SequenceEqual(tiles2));
		}
		#endregion

		#region DrawTiles

		[TestCase(1, 107, 1, TestName="TileBag_DrawTiles_1")]
		[TestCase(2, 106, 2, TestName="TileBag_DrawTiles_2")]
		[TestCase(3, 105, 3, TestName="TileBag_DrawTiles_3")]
		[TestCase(4, 104, 4, TestName="TileBag_DrawTiles_4")]
		[TestCase(5, 103, 5, TestName="TileBag_DrawTiles_5")]
		[TestCase(108, 0, 108, TestName="TileBag_DrawTiles_108")]
		[TestCase(6, 102, 6, TestName="TileBag_DrawTiles_6")]
		[TestCase(0, 108, 0, TestName="TileBag_DrawTiles_0")]
		[TestCase(109, 0, 108, TestName="TileBag_DrawTiles_109")]
		public void TileBag_DrawTiles(int tilesToDraw, int expectedResult, int expectedDrawnTiles)
		{
			// Arrange
			var tileBag = new TileBag();

			// Act
			var drawnTiles = tileBag.DrawTiles(tilesToDraw);

			// Assert
			Assert.AreEqual(expectedResult, tileBag.Tiles.Count);
			Assert.AreEqual(expectedDrawnTiles, drawnTiles.Count);
		}


		#endregion

	}
}
