using System;
using System.Collections.Generic;

namespace Models.ClientCommands
{
    public class SwapTilesTurn
    {
        public Guid GameId;
        public List<Tile> TurnedInTiles { get; set; }
    }
}