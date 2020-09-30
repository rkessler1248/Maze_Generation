using System;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class AldousBroderWallCarverTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_AldousBroderWallCarver_LinkToCurrent()
        {
            AldousBroderWallCarver carver = new AldousBroderWallCarver();

            Cell cell1 = new Cell( 0, 0 );
            Assert.True( carver.LinkToCurrent( cell1 ) );

            Cell cell2 = new Cell( 0, 1 );
            cell1.LinkTo( cell2 );
            
            Assert.False( carver.LinkToCurrent( cell1 ) );
            Assert.False( carver.LinkToCurrent( cell2 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_AldousBroderWallCarver_Carve()
        {
            const int certaintyIterations = 10_000;

            AldousBroderWallCarver carver = new AldousBroderWallCarver();

            for ( int i = 0; i < certaintyIterations; ++i )
            {
                Maze maze = new Maze( 3, 3 );

                try
                {
                    carver.Carve( maze );
                }
                catch ( Exception ex )
                {
                    Assert.True( false, $"Creator threw exception: {ex}" );
                }

                for ( int row = 0; row < maze.NumberOfRows; ++row )
                for ( int column = 0; column < maze.NumberOfColumns; ++column )
                {
                    Assert.NotEmpty( maze[ row, column ].Links );
                }
            }
        }
    }
}