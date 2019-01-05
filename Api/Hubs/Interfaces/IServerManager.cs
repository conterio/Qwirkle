using Models;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface IServerManager
    {
        bool Register(string playerName);
        List<Game> AvailableGames();
        Game CreateGame(GameSettings settings);
    }
}