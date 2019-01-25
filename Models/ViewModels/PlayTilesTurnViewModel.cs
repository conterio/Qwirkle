using System;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class PlayTilesTurnViewModel
    {
        public Guid GameId;
        public List<TilePlacement> Placements;
    }
}