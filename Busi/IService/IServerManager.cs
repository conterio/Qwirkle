using Models;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface IServerManager
    {
        bool Register(string playerName);
        List<Game> AvailableGames();
        Game CreateGame(GameSettings settings);
    }
}