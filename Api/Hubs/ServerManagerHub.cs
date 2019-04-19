using Busi.IBusi;
using Busi.IService;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.ClientCommands;
using Models.ClientOutBound;
using System;
using System.Collections.Generic;


namespace Api
{
    public class ServerManagerHub : Hub, IServerManager, ILobbyManager, IGameManager
    {
        private readonly IPlayerBusi _playerBusi;
        private readonly IGameBusi _gameBusi;

        public ServerManagerHub(IPlayerBusi playerBusi,
            IGameBusi gameBusi)
        {
            _playerBusi = playerBusi;
            _gameBusi = gameBusi;
        }

		public string GetPlayerId()
		{
			return Context.ConnectionId;
		}

        public void SignalAddPlayer(Guid gameId, string playerConnectionId)
        {
            Clients.Client(playerConnectionId).SendAsync(nameof(IAIManager.JoinGame), gameId).GetAwaiter().GetResult();
        }

        public List<AvailableGameViewModel> AvailableGames()
        {
            return _gameBusi.GetLobbies();
        }

        public AvailableGameViewModel CreateGame(GameSettings settings)
        {
			return  _gameBusi.CreateGame(settings, Context.ConnectionId);
        }

        public List<PlayerViewModel> GetAvailablePlayers()
        {
            return _playerBusi.GetAllPlayers();
        }

        public void JoinGame(Guid gameId)
        {
            _gameBusi.AddPlayer(gameId, Context.ConnectionId);
            Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString()).GetAwaiter().GetResult();
        }

		public void LeaveGame(Guid gameId)
        {
            _gameBusi.RemovePlayer(gameId, Context.ConnectionId);
        }

        public void Register(string playerName, bool isHumanPlayer)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return;
            }
            _playerBusi.AddPlayer(Context.ConnectionId, playerName, isHumanPlayer);
        }

        public void StartGame(Guid gameId)
        {
            _gameBusi.StartGame(gameId);
        }

        public void PlayTiles(PlayTilesTurn turn)
        {
            _gameBusi.PlayTiles(Context.ConnectionId, turn);
        }

        public void SwapTiles(SwapTilesTurn turn)
        {
            _gameBusi.SwapTiles(Context.ConnectionId, turn);
        }
    }
}