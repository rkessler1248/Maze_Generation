using Xunit;

namespace Mazes.Structures.Tests
{
    public class MazeTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Maze_Construction()
        {
            Maze maze = new Maze( 3, 4 );

            Assert.Equal( 3, maze.NumberOfRows );
            Assert.Equal( 4, maze.NumberOfColumns );
            Assert.Equal( 12, maze.Size );

            for ( int row = 0; row < maze.NumberOfRows; ++row )
            for ( int column = 0; column < maze.NumberOfColumns; ++column )
            {
                Cell current = maze[ row, column ];
                Assert.NotNull( current );

                Assert.Equal( row, current.Row );
                Assert.Equal( column, current.Column );

                Assert.Empty( current.Links() );

                if ( row > 0 )
                {
                    Cell northern = maze[ row - 1, column ];
                    Assert.True( current.IsNeighbor( northern ) );
                }

                if ( row < maze.NumberOfRows - 1 )
                {
                    Cell southern = maze[ row + 1, column ];
                    Assert.True( current.IsNeighbor( southern ) );
                }

                if ( column > 0 )
                {
                    Cell western = maze[ row, column - 1 ];
                    Assert.True( current.IsNeighbor( western ) );
                }

                if ( column < maze.NumberOfColumns - 1 )
                {
                    Cell eastern = maze[ row, column + 1 ];
                    Assert.True( current.IsNeighbor( eastern ) );
                }
            }
        }
    }
}