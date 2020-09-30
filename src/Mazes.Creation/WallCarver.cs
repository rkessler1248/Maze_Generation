using System;
using Mazes.Structures;

namespace Mazes.Creation
{
    public abstract class WallCarver
    {
        protected readonly Random RNG = new Random( ( int ) DateTime.UtcNow.Ticks );
        public abstract void Carve( Maze maze );
    }
}