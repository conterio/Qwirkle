using Busi;
using Busi.IService;
using Microsoft.AspNetCore.SignalR;
using Models.EventModels;
using Newtonsoft.Json;

namespace Api.Updaters
{
    public class Updater : IUpdater
    {
        private readonly IHubContext<ServerManagerHub> _hubContext;

        public Updater(IHubContext<ServerManagerHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public void UpdateGroupTurnEvent(string groupId, TurnEvent payload)
        {
            _hubContext.Clients.Group(groupId).SendAsync(nameof(IGameActions.TurnEvent), payload);
        }

        public void UpdateClientEndTurnEvent(string connectionId, EndTurnEvent payload)
        {
            _hubContext.Clients.Client(connectionId).SendAsync(nameof(IGameActions.EndTurnEvent), payload);
        }

		public void GameInfoEvent(string connectionId, GameInfoEvent payload)
		{
			_hubContext.Clients.Client(connectionId).SendAsync(nameof(GameInfoEvent), payload);
		}

		public void PlayerJoinedGameLobbyEvent(string groupId, PlayerJoinedEvent payload)
		{
			_hubContext.Clients.Group(groupId).SendAsync(nameof(PlayerJoinedEvent), payload);
		}

		public void StartGameEvent(string groupId, StartGameEvent payload)
		{
			_hubContext.Clients.Group(groupId).SendAsync(nameof(StartGameEvent), payload);
		}

		public void EndGameEvent(string groupId, EndGameEvent payload)
		{
			_hubContext.Clients.Group(groupId).SendAsync(nameof(EndGameEvent), payload);
		}

		public void PlayerRemoveEvent(string groupId, PlayerRemovedEvent payload)
		{
			throw new System.NotImplementedException();
		}
	}
}