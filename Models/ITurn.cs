using System.Collections.Generic;

namespace Models
{
    public interface ITurn
    {
        List<Tile> NewTiles { get; set; }
    }
}