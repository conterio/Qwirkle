using Models;
using Models.ViewModels;

namespace Api.Hubs.Interfaces
{
    public interface IGameActionsHub
    {
        void GameStarted(GameViewModel game);
        void LobbyState();
        void SignalTurn(GameViewModel game);
        void SignalInvalid(string reason);
        void GameOver(GameViewModel game);
    }
}