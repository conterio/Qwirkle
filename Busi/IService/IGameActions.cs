using Models.EventModels;

namespace Busi.IService
{
    public interface IGameActions
    {
        void LobbyEvent();
        void TurnEvent(TurnEvent turn);
        void EndTurnEvent(EndTurnEvent endTurn);
        void GameStarted();
        void GameOver();
    }
}