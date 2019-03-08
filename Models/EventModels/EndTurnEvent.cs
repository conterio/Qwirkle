using System.Collections.Generic;

namespace Models.EventModels
{
    public class EndTurnEvent
    {
        public List<Tile> NewTiles { get; set; }
    }
}