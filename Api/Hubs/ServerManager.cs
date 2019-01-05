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
        private readonly IGameRepository _gameRepository;

        ServerManager(IPlayerRepository playerRepository,
            IGameRepository gameRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
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
                var game = _gameRepository.GetGame(gameId);
                game.Players.Add(player);
                Groups.AddToGroupAsync(player.ConnectionId, gameId.ToString()).GetAwaiter().GetResult();
                Clients.Group(gameId.ToString()).SendAsync(nameof(IGameActions.LobbyState), game.GetViewModel());
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

        public void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}