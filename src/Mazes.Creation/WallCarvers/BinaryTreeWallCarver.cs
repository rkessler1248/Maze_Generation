using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public class BinaryTreeWallCarver : WallCarver
    {
        public override void Carve( Maze maze )
        {
            for ( int row = 0; row < maze.NumberOfRows; ++row )
            for ( int column = 0; column < maze.NumberOfColumns; ++column )
            {
                Cell link = ChooseLink( maze, row, column );
                if ( link != null )
                {
                    maze[ row, column ].LinkTo( link );
                }
            }
        }

        public Cell ChooseLink( Maze maze, in int row, in int column )
        {
            Cell result;
            if ( column == maze.NumberOfColumns - 1 && row == maze.NumberOfRows - 1 )
            {
                result = null;
            }
            else if ( column == maze.NumberOfColumns - 1 )
            {
                result = maze[ row + 1, column ];
            }
            else if ( row == maze.NumberOfRows - 1 )
            {
                result = maze[ row, column + 1 ];
            }
            else
            {
                result = Random( maze[ row + 1, column ], maze[ row, column + 1 ] );
            }

            return result;
        }
    }
}