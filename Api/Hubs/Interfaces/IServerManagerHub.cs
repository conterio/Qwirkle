using Models;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface IServerManagerHub
    {
        bool Register(string playerName);
        List<Game> AvailableGames();
        Game CreateGame(GameSettings settings);
    }
}