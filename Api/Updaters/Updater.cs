using Busi.IService;
using Busi.IUpdater;
using Microsoft.AspNetCore.SignalR;
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
        public void UpdateGroupTurnEvent(string groupId, object payload)
        {
            _hubContext.Clients.Group(groupId).SendAsync(nameof(IGameActions.TurnEvent), JsonConvert.SerializeObject(payload));
        }

        public void UpdateClientEndTurnEvent(string connectionId, object payload)
        {
            _hubContext.Clients.Client(connectionId).SendAsync(nameof(IGameActions.EndTurnEvent), JsonConvert.SerializeObject(payload));
        }
    }
}