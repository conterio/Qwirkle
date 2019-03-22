using Models;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface IServerManager
    {
        bool Register(string playerName, bool isHumanPlayer);
        List<Game> AvailableGames();
        Game CreateGame(GameSettings settings);
    }
}