using System;
using Mazes.Structures;

namespace Mazes.Creation
{
    public class BinaryTreeCreator
    {
        private readonly Random _random;
        private readonly Maze _maze;

        public BinaryTreeCreator( Maze maze, int? randomSeed = null )
        {
            _maze = maze;
            _random = new Random( randomSeed ?? DateTime.UtcNow.Millisecond );
        }

        public void Carve()
        {
            for ( int row = 0; row < _maze.Rows; ++row )
            for ( int column = 0; column < _maze.Columns; ++column )
            {
                Cell current = _maze[ row, column ];
                Cell link = ChooseLink( current );

                if ( link != null )
                {
                    current.LinkTo( link );
                }
            }
        }

        public Cell ChooseLink( Cell cell )
        {
            int row, column;
            if ( cell.Column == _maze.Columns - 1 )
            {
                ( row, column ) = ( cell.Row + 1, cell.Column );
            }
            else if ( cell.Row == _maze.Rows - 1 )
            {
                ( row, column ) = ( cell.Row, cell.Column + 1 );
            }
            else
            {
                ( row, column ) = _random.Next( 0, 2 ) == 0
                                      ? ( cell.Row + 1, cell.Column )
                                      : ( cell.Row, cell.Column + 1 );
            }

            return cell.GetNeighbor( row, column );
        }
    }
}