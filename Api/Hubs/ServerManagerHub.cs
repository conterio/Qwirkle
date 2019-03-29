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

        public bool SignalAddPlayer(Guid gameId, string playerConnectionId)
        {
            try
            {
                Clients.Client(playerConnectionId).SendAsync(nameof(IAIManager.JoinGame), gameId).GetAwaiter().GetResult();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        public bool Register(string playerName, bool isHumanPlayer)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return false;
            }
            _playerBusi.AddPlayer(Context.ConnectionId, playerName, isHumanPlayer);
            return true;
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