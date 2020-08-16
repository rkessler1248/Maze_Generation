using Xunit;

namespace Mazes.Structures.Tests
{
    public class MazeTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_Maze_Cells()
        {
            var maze = new Maze( 4, 4 );

            Assert.Equal( 4, maze.Columns );
            Assert.Equal( 4, maze.Rows );

            for ( int row = 0; row < maze.Rows; ++row )
            for ( int column = 0; column < maze.Columns; ++column )
            {
                Cell current = maze[ row, column ];
                Assert.Equal( row, current.Row );
                Assert.Equal( column, current.Column );
            }
        }
        
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_Maze_CellNeighbors()
        {
            var maze = new Maze( 4, 4 );

            for ( int row = 0; row < maze.Rows; ++row )
            for ( int column = 0; column < maze.Columns; ++column )
            {
                Cell current = maze[ row, column ];
                if ( row > 0 )
                {
                    Cell neighbor = maze[ row - 1, column ];
                    Assert.Equal( current.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( current ) );
                }

                if ( row < maze.Rows - 1 )
                {
                    Cell neighbor = maze[ row + 1, column ];
                    Assert.Equal( current.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( current ) );
                }

                if ( column > 0 )
                {
                    Cell neighbor = maze[ row, column - 1 ];
                    Assert.Equal( current.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( current ) );
                }

                if ( column < maze.Columns - 1 )
                {
                    Cell neighbor = maze[ row, column + 1 ];
                    Assert.Equal( current.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( current ) );
                }
            }
        }
    }
}