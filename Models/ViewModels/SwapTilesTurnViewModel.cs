using System;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class SwapTilesTurnViewModel
    {
        public Guid GameId;
        public List<Tile> TurnedInTiles { get; set; }
    }
}