using Models.EventModels;

namespace Busi
{
    /// <summary>
    /// IUpdater is an interface for outgoing calls. All return types must be void
    /// </summary>
    public interface IUpdater
    {
		//gamelobby
		void GameInfoEvent(string connectionId, GameInfoEvent payload);
		void PlayerJoinedGameLobbyEvent(string groupId, PlayerJoinedEvent payload);

		//during game
		void StartGameEvent(string groupId, StartGameEvent payload);
        void UpdateClientEndTurnEvent(string connectionId, EndTurnEvent payload);
        void UpdateGroupTurnEvent(string groupId, TurnEvent payload);
		void PlayerRemoveEvent(string groupId, PlayerRemovedEvent payload);
		void EndGameEvent(string groupId, EndGameEvent payload);
    }
}