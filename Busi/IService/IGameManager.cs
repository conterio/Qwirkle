using Models.ViewModels;

namespace Busi.IService
{
    public interface IGameManager
    {
        void PlayTiles(PlayTilesTurnViewModel turn);
        void SwapTiles(SwapTilesTurnViewModel turn);
    }
}