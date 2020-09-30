using System;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class SidewinderWallCarverTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderWallCarver_ContinueRun_NorthernBorder()
        {
            Maze maze = new Maze( 3, 3 );
            SidewinderWallCarver carver = new SidewinderWallCarver();
            Assert.True( carver.ContinueRun( maze, 2, 0 ) );
            Assert.True( carver.ContinueRun( maze, 2, 1 ) );
            Assert.False( carver.ContinueRun( maze, 2, 2 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderWallCarver_ContinueRun_EasternBorder()
        {
            Maze maze = new Maze( 3, 3 );
            SidewinderWallCarver carver = new SidewinderWallCarver();
            Assert.False( carver.ContinueRun( maze, 0, 2 ) );
            Assert.False( carver.ContinueRun( maze, 1, 2 ) );
            Assert.False( carver.ContinueRun( maze, 2, 2 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderWallCarver_Carve()
        {
            const int certaintyIterations = 10_000;
            
            SidewinderWallCarver carver = new SidewinderWallCarver();
            
            for ( int i = 0; i < certaintyIterations; ++i )
            {
                Maze maze = new Maze( 5, 5 );

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