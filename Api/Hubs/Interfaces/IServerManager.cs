using Models;
using Models.Interfaces;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface IServerManager
    {
        bool Register(string playerName);
        List<Game> AvailableGames();
        GameSettings CreateGame(GameSettings settings);
    }
}