using Busi.IBusi;
using Busi.IRepo;
using Busi.IService;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.ClientCommands;
using System;
using System.Collections.Generic;

namespace Api
{
    public class ServerManagerHub : Hub, IServerManager, ILobbyManager, IGameManager
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameBusi _gameBusi;

        public ServerManagerHub(IPlayerRepository playerRepository,
            IGameRepository gameRepository,
            IGameBusi gameBusi)
        {
            _playerRepository = playerRepository; //TODO remove player repository or make it a singleton
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

        public List<Game> AvailableGames()
        {
            //TODO do we want to return the whole game object?
            return _gameBusi.GetLobbies();
        }

        public Game CreateGame(GameSettings settings)
        {
            //TODO do we want to return the whole game object?
            var game = _gameBusi.CreateGame(settings);
            if (!JoinGame(game.GameId, Context.ConnectionId))
            {
                //TODO if we don't join the game should we delete it and fail the call?
            }
            return game;
        }

        public List<Player> GetAvailablePlayers()
        {
            //TODO do we want to return the whole player object?
            return _playerRepository.GetAllPlayers();
        }

        public bool JoinGame(Guid gameId, string playerConnectionId)
        {
            try
            {
                _gameBusi.AddPlayer(gameId, playerConnectionId);
                Groups.AddToGroupAsync(playerConnectionId, gameId.ToString()).GetAwaiter().GetResult();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Register(string playerName, bool isHumanPlayer)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return false;
            }
            _playerRepository.AddPlayer(Context.ConnectionId, playerName, isHumanPlayer);
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