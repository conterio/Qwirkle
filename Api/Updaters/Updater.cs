using Busi.IService;
using Busi.IUpdater;
using Microsoft.AspNetCore.SignalR;

namespace Api.Updaters
{
    public class Updater : IUpdater
    {
        private readonly IHubContext<ServerManagerHub> _hubContext;

        public Updater(IHubContext<ServerManagerHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public void UpdateGroup(string groupId, object payload)
        {
            _hubContext.Clients.Group(groupId).SendAsync(nameof(IGameActions.SignalGameState), payload);
        }

		public void UpdateGroupTurnPlayed(string groupId, object payload)
		{
			_hubContext.Clients.Group(groupId).SendAsync(nameof(IGameActions.SignalTurnPlayed), payload);
		}

        public void UpdateClient(string connectionId, object payload)
        {

        }
    }
}