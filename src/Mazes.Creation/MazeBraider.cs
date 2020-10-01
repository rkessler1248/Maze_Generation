using System;
using System.Collections.Generic;
using System.Linq;
using Mazes.Structures;
using Mazes.Utility;

namespace Mazes.Creation
{
    public class MazeBraider
    {
        private readonly IRandomNumberGenerator _random = new BasicRandomNumberGenerator();

        public void Braid( Maze maze, float braidPercent )
        {
            IList<Cell> deadEnds = maze.DeadEnds().ToList();
            Shuffle( deadEnds );

            int braidCount = ( int ) Math.Round( deadEnds.Count * braidPercent );
            for ( int i = 0; i < braidCount; ++i )
            {
                deadEnds[ i ].LinkTo( RandomNeighbor( deadEnds[ i ] ) );
            }
        }

        private Cell RandomNeighbor( Cell cell )
        {
            if ( cell.Neighbors.Count == 0 )
            {
                return null;
            }

            Cell result;
            do
            {
                result = cell.Neighbors.ElementAt( _random.Next( 0, cell.Neighbors.Count ) );

                if ( result.IsLinkedTo( cell ) )
                {
                    result = null;
                }
            } while ( result is null );

            return result;
        }

        private void Shuffle( IList<Cell> list )
        {
            int current = list.Count;
            while ( current > 1 )
            {
                --current;

                int swap = _random.Next( 0, current + 1 );

                Cell temp = list[ swap ];
                list[ swap ] = list[ current ];
                list[ current ] = temp;
            }
        }
    }
}