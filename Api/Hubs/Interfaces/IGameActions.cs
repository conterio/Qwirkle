using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Hubs.Interfaces
{
    public interface IGameActions
    {
        ITurn SignalTurn(GameState gameState);
        void SignalInvalid(string reason);
        void GameOver(string results);
        //Todo: more

    }
}
