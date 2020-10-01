using System;

namespace Mazes.Utility
{
    public class BasicRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random = new Random( ( int ) DateTime.UtcNow.Ticks );
        public int Next( int min, int max ) => _random.Next( min, max );
    }
}