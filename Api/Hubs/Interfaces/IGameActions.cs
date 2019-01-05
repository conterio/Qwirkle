using Models;

namespace Api.Hubs.Interfaces
{
    public interface IGameActions
    {
        void LobbyState();
        void SignalTurn(GameState gameState);
        void SignalInvalid(string reason);
        void GameOver(string results);
    }
}