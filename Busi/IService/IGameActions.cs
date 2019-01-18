using Models.ViewModels;

namespace Busi.IService
{
    public interface IGameActions
    {
        void GameStarted(GameViewModel game);
        void LobbyState();
        void SignalTurn(GameViewModel game);
        void SignalInvalid(string reason);
        void GameOver(GameViewModel game);
    }
}