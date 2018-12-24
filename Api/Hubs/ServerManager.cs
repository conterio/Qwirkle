using Api.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Interfaces;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api
{
    public class ServerManager : Hub, IServerManager, ILobbyManager
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;

        public ServerManager(IPlayerRepository playerRepository,
            IGameRepository gameRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public bool AddPlayer()
        {
            throw new NotImplementedException();
        }

        public List<Game> AvailableGames()
        {
            return _gameRepository.GetLobbies();
        }

        public Game CreateGame(GameSettings settings)
        {
            return _gameRepository.CreateGame(settings);
        }

        public List<IPlayer> GetAvailablePlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        public bool JoinGame(Guid gameId, IPlayer player)
        {
            throw new NotImplementedException();
        }

        public bool Register(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return false;
            }
            _playerRepository.AddPlayer(Context.ConnectionId, playerName);
            return true;
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        GameSettings IServerManager.CreateGame(GameSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}