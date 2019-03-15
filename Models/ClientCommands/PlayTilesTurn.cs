using System;
using System.Collections.Generic;

namespace Models.ClientCommands
{
    public class PlayTilesTurn
    {
        public Guid GameId;
        public List<TilePlacement> Placements;
    }
}