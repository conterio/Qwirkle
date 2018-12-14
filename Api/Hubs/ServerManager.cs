using Api.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Interfaces;
using Repository;
using System;
using System.Collections.Generic;

namespace Api
{
    public class ServerManager : Hub, IServerManager, ILobbyManager
    {
        private readonly IPlayerRepository _playerRepository;
        ServerManager(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool AddPlayer()
        {
            throw new NotImplementedException();
        }

        public List<GameSettings> AvailableGames()
        {
            throw new NotImplementedException();
        }

        public GameSettings CreateGame(GameSettings settings)
        {
            throw new NotImplementedException();
        }

        public List<IPlayer> GetAvailableAIPlayers()
        {
            throw new NotImplementedException();
        }

        public bool JoinGame(Guid gameId, IPlayer player)
        {
            throw new NotImplementedException();
        }

        public bool Register(IPlayer player)
        {
            if (string.IsNullOrWhiteSpace(player.Name))
            {
                return false;
            }
            _playerRepository.AddPlayer(Context.ConnectionId, player);
            return true;
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}