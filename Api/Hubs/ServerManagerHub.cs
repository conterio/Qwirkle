using Busi.IBusi;
using Busi.IRepo;
using Busi.IService;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Api
{
    public class ServerManagerHub : Hub, IServerManager, ILobbyManager
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGameBusi _gameBusi;

        ServerManagerHub(IPlayerRepository playerRepository,
            IGameRepository gameRepository,
            IGameBusi gameBusi)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gameBusi = gameBusi;
        }

        public bool SignalAddPlayer(Guid gameId, IPlayer player)
        {
            try
            {
                Clients.Client(player.ConnectionId).SendAsync(nameof(IAIManager.JoinGame), gameId).GetAwaiter().GetResult();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            try
            {
                _gameBusi.AddPlayer(gameId, player);
                Groups.AddToGroupAsync(player.ConnectionId, gameId.ToString()).GetAwaiter().GetResult();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        public void StartGame(Guid gameId)
        {
            _gameBusi.StartGame(gameId);
        }
    }
}