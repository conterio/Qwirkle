using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Hubs.Interfaces
{
    public interface ILobbyManager
    {
        GameSettings CreateGame(GameSettings settings);
        List<GameSettings> AvailableGames();
        bool JoinGame(GameSettings settings, IPlayer player);
    }
}
