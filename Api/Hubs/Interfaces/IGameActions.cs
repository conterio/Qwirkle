using Models;

namespace Api.Hubs.Interfaces
{
    public interface IGamections
    {
        ITurn SignalTurn(GameState gameState);
        void SignalInvalid(string reason);
        void GameOver(string results);
        //Todo: a lot more
    }
}