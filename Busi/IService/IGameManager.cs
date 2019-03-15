using Models.ClientCommands;

namespace Busi.IService
{
    public interface IGameManager
    {
        void PlayTiles(PlayTilesTurn turn);
        void SwapTiles(SwapTilesTurn turn);
    }
}