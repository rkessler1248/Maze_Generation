using Xunit;

namespace Mazes.Structures.Tests
{
    public class MazeTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_ProtoMaze_Cells()
        {
            var maze = new ProtoMaze( 4, 4 );

            Assert.Equal( 4, maze.Columns );
            Assert.Equal( 4, maze.Rows );

            Assert.True( maze.IsCornerCell( maze[ 0, 0 ] ) );

            for ( int row = 0; row < maze.Rows; ++row )
            for ( int column = 0; column < maze.Columns; ++column )
            {
                ProtoCell cell = maze[ row, column ];
                Assert.Equal( row, cell.Row );
                Assert.Equal( column, cell.Column );
            }
        }
        
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_ProtoMaze_CellNeighbors()
        {
            var maze = new ProtoMaze( 4, 4 );

            for ( int row = 0; row < maze.Rows; ++row )
            for ( int column = 0; column < maze.Columns; ++column )
            {
                ProtoCell cell = maze[ row, column ];
                
                int expectedNeighborCount = maze.IsCornerCell( cell ) ? 2
                                            : maze.IsEdgeCell( cell ) ? 3 : 4;
                Assert.Equal( expectedNeighborCount, cell.Neighbors.Count );

                if ( row > 0 )
                {
                    ProtoCell neighbor = maze[ row - 1, column ];
                    Assert.Equal( cell.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( cell ) );
                }

                if ( row < maze.Rows - 1 )
                {
                    ProtoCell neighbor = maze[ row + 1, column ];
                    Assert.Equal( cell.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( cell ) );
                }

                if ( column > 0 )
                {
                    ProtoCell neighbor = maze[ row, column - 1 ];
                    Assert.Equal( cell.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( cell ) );
                }

                if ( column < maze.Columns - 1 )
                {
                    ProtoCell neighbor = maze[ row, column + 1 ];
                    Assert.Equal( cell.IsNeighborOf( neighbor ), neighbor.IsNeighborOf( cell ) );
                }
            }
        }
    }
}