﻿using Models.ViewModels;

namespace Busi.IService
{
    public interface IGameActions
    {
        void SignalGameState(GameViewModel game);
    }
}