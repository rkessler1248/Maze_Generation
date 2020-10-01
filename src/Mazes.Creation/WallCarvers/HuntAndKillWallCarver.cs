using System.Linq;
using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public class HuntAndKillWallCarver : WallCarver
    {
        public override void Carve( Maze maze )
        {
            Cell current = Random( maze );
            while ( current != null )
            {
                Cell next = Random( current.Neighbors, current.Neighbors.Count );
                if ( Visited( next ) )
                {
                    current = Hunt( maze );
                    current?.LinkTo( current.Neighbors.First() );
                }
                else
                {
                    current.LinkTo( next );
                    current = next;
                }
            }
        }

        public Cell Hunt( Maze maze )
        {
            for ( int row = maze.NumberOfRows - 1; row > -1; --row )
            for ( int column = maze.NumberOfColumns - 1; column > -1; --column )
            {
                if ( Visited( maze[ row, column ] ) )
                {
                    continue;
                }

                foreach ( Cell neighbor in maze[ row, column ].Neighbors )
                {
                    if ( Visited( neighbor ) )
                    {
                        return maze[ row, column ];
                    }
                }
            }

            return null;
        }
    }
}