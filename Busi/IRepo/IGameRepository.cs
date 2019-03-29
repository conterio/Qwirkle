using Models;
using System;
using System.Collections.Generic;

namespace Busi.IRepo
{
    public interface IGameRepository
    {
        Game CreateGame(GameSettings gameSettings);
        Game GetGame(Guid gameId);
        List<Game> GetLobbies();
		void CleanUpGame(Guid gameId);
    }
}