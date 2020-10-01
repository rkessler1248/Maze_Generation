using System.Collections.Generic;
using System.Linq;
using Mazes.Creation.Utility;
using Mazes.Structures;

namespace Mazes.Creation
{
    public abstract class MazeCreator
    {
        protected readonly IRandomNumberGenerator RNG = new BasicRandomNumberGenerator();

        protected Cell Random( Cell cell1, Cell cell2 ) => RNG.Next( 0, 2 ) == 0 ? cell1 : cell2;

        protected Cell Random( IEnumerable<Cell> set, int elements ) => set.ElementAtOrDefault( RNG.Next( 0, elements ) );

        protected Cell Random( Maze maze ) => maze is MaskedMaze masked ? Random( masked ) : maze[ RNG.Next( 0, maze.NumberOfRows ), RNG.Next( 0, maze.NumberOfColumns ) ];

        protected abstract bool Visited( Cell cell );

        private Cell Random( MaskedMaze maze )
        {
            Cell result;
            do
            {
                result = maze[ RNG.Next( 0, maze.NumberOfRows ), RNG.Next( 0, maze.NumberOfColumns ) ];
            } while ( result is null );

            return result;
        }
    }
}