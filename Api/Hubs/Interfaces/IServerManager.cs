using Models;
using Models.Interfaces;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface IServerManager
    {
        bool Register(IPlayer Player);
        List<GameSettings> AvailableGames();
        GameSettings CreateGame(GameSettings settings);

    }
}