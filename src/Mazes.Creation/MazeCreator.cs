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

        protected Cell Random( IEnumerable<Cell> set, int elements ) => set.ElementAtOrDefault( RNG.Next( 0, elements ) );

        protected Cell Random( Maze maze ) => maze is MaskedMaze masked ? Random( masked ) : maze[ RNG.Next( 0, maze.NumberOfRows ), RNG.Next( 0, maze.NumberOfColumns ) ];

        protected bool Visited( Cell cell ) => cell.Links.Count > 0;

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