using System;
using System.Collections.Generic;
using System.Linq;
using Mazes.Structures;

namespace Mazes.Creation
{
    public class AldousBroderCreator
    {
        private readonly Random _random = new Random( ( int ) DateTime.UtcNow.Ticks );

        public void Carve( Maze maze )
        {
            long unvisited = maze.Size - 1;
            Cell current = Random( maze );
            while ( unvisited > 0 )
            {
                Cell candidate = Random( current.Neighbors );
                if ( LinkToCurrent( candidate ) )
                {
                    candidate.LinkTo( current );
                    --unvisited;
                }

                current = candidate;
            }
        }

        public bool LinkToCurrent( Cell candidate )
        {
            return candidate.Links.Count == 0;
        }

        private Cell Random( Maze maze )
        {
            return maze[ _random.Next( 0, maze.NumberOfRows ), _random.Next( 0, maze.NumberOfColumns ) ];
        }

        private Cell Random( IReadOnlyCollection<Cell> cells )
        {
            return cells.ElementAt( _random.Next( 0, cells.Count ) );
        }
    }
}