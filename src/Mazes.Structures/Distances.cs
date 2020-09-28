using System.Collections.Generic;

namespace Mazes.Structures
{
    public class Distances
    {
        private readonly IDictionary<Cell, int> _cells;

        private Distances( Cell root )
        {
            _cells = new Dictionary<Cell, int> { { root, 0 } };
        }

        public int this[ Cell cell ]
        {
            get => _cells.ContainsKey( cell ) ? _cells[ cell ] : -1;
            private set => _cells[ cell ] = value;
        }
        
        public static Distances CalculateDijkstra( Cell root )
        {
            Distances result = new Distances( root );

            Queue<Cell> frontier = new Queue<Cell>();
            frontier.Enqueue( root );

            while ( frontier.Count > 0 )
            {
                Cell current = frontier.Dequeue();
                foreach ( Cell link in current.Links() )
                {
                    if ( result[ link ] == -1 )
                    {
                        result[ link ] = result[ current ] + 1;
                        frontier.Enqueue( link );
                    }
                }
            }

            return result;
        }
    }
}