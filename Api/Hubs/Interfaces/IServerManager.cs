﻿using Models;
using Models.Interfaces;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface IServerManager
    {
        void Register(IPlayer Player);
        List<GameSettings> AvailableGames();
        GameSettings CreateGame(GameSettings settings);

    }
}