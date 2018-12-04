using Models;
using System;

namespace Repository
{
    public interface IGameRepository
    {
        Game GetGame(Guid guid);
    }

    public class GameRepository : IGameRepository
    {
        public Game GetGame(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}