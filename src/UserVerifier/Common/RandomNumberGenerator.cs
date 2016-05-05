using System;

namespace UserVerifier.Common
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        readonly Random _random = new Random();

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }
}