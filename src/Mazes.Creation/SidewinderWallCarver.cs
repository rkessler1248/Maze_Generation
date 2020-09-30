using System.Collections.Generic;
using Mazes.Structures;

namespace Mazes.Creation
{
    public class SidewinderWallCarver : WallCarver
    {
        public override void Carve( Maze maze )
        {
            IList<Cell> run = new List<Cell>();

            for ( int row = 0; row < maze.NumberOfRows; ++row )
            for ( int column = 0; column < maze.NumberOfColumns; ++column )
            {
                run.Add( maze[ row, column ] );
                
                if ( ContinueRun( maze, row, column ) )
                {
                    maze[ row, column ].LinkTo( maze[ row, column + 1 ] );
                }
                else
                {
                    if ( row != maze.NumberOfRows - 1 )
                    {
                        Cell link = run[ RNG.Next( 0, run.Count ) ];
                        link.LinkTo( maze[ link.Row + 1, link.Column ] );
                    }

                    run.Clear();
                }
            }
        }

        public bool ContinueRun( Maze maze, int row, int column )
        {
            bool result;
            if ( row == maze.NumberOfRows - 1 && column == maze.NumberOfColumns - 1 )
            {
                result = false;
            }
            else if ( row == maze.NumberOfRows - 1 )
            {
                result = true;
            }
            else if ( column == maze.NumberOfColumns - 1 )
            {
                result = false;
            }
            else
            {
                result = RNG.Next( 0, 2 ) == 0;
            }

            return result;
        }
    }
}