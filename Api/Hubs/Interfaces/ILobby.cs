using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface ILobby
    {        
        void StartGame();
        bool AddPlayer(); 
        List<IPlayer> GetAvailableAIPlayers();
    }
}
