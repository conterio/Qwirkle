using Models.EventModels;

namespace Busi.IService
{
    public interface IGameActions
    {
        void TurnEvent(TurnEvent turn);
        void EndTurnEvent(EndTurnEvent endTurn);
        void GameStartedEvent(StartGameEvent startGameEvent);
        void GameOverEvent(EndGameEvent endGameEvent);
		void GameInfoEvent(GameInfoEvent gameInfoEvent);
		void PlayerJoinedEvent(PlayerJoinedEvent playerJoinedEvent);
		void PlayerRemovedEvent(PlayerRemovedEvent playerRemovedEvent);
	}
}