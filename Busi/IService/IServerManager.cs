using Models;
using Models.ClientOutBound;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface IServerManager
    {
		string GetPlayerId();
        void Register(string playerName, bool isHumanPlayer);
        List<AvailableGameViewModel> AvailableGames();
        AvailableGameViewModel CreateGame(GameSettings settings);
    }
}