using Models;
using Models.ClientOutBound;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface IServerManager
    {
        bool Register(string playerName, bool isHumanPlayer);
        List<AvailableGameViewModel> AvailableGames();
        AvailableGameViewModel CreateGame(GameSettings settings);
    }
}