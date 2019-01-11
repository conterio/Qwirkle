using System;
using System.Collections.Generic;
using System.Linq;

namespace Busi.Helpers
{
    public interface IShuffleHelper
    {
        void Shuffle<T>(List<T> list);
    }
    public class ShuffleHelper : IShuffleHelper
    {
        [ThreadStatic]
        private readonly Random _random = new Random();

        public void Shuffle<T>(List<T> list)
        {
            list = list.OrderBy(_ => _random.Next()).ToList();
        }
    }
}