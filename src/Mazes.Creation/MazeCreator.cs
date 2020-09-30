using System;
using System.Collections.Generic;
using System.Linq;
using Mazes.Structures;

namespace Mazes.Creation
{
    public abstract class MazeCreator
    {
        protected readonly Random RNG = new Random( ( int ) DateTime.UtcNow.Ticks );

        protected Cell Random( Cell cell1, Cell cell2 ) => RNG.Next( 0, 2 ) == 0 ? cell1 : cell2;

        protected Cell Random( IEnumerable<Cell> set, int elements )
        {
            return set.ElementAt( RNG.Next( 0, elements ) );
        }

        protected Cell Random( Maze maze )
        {
            return maze[ RNG.Next( 0, maze.NumberOfRows ), RNG.Next( 0, maze.NumberOfColumns ) ];
        }
    }
}