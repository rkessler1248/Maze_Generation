using System;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class SidewinderCreatorTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderCreator_ContinueRun_NorthernBorder()
        {
            Maze maze = new Maze( 3, 3 );
            SidewinderCreator creator = new SidewinderCreator();
            Assert.True( creator.ContinueRun( maze, 2, 0 ) );
            Assert.True( creator.ContinueRun( maze, 2, 1 ) );
            Assert.False( creator.ContinueRun( maze, 2, 2 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderCreator_ContinueRun_EasternBorder()
        {
            Maze maze = new Maze( 3, 3 );
            SidewinderCreator creator = new SidewinderCreator();
            Assert.False( creator.ContinueRun( maze, 0, 2 ) );
            Assert.False( creator.ContinueRun( maze, 1, 2 ) );
            Assert.False( creator.ContinueRun( maze, 2, 2 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderCreator_Carve()
        {
            const int certaintyIterations = 10_000;
            
            SidewinderCreator creator = new SidewinderCreator();
            
            for ( int i = 0; i < certaintyIterations; ++i )
            {
                Maze maze = new Maze( 5, 5 );

                try
                {
                    creator.Carve( maze );
                }
                catch ( Exception ex )
                {
                    Assert.True( false, $"Creator threw exception: {ex}" );
                }
                
                for ( int row = 0; row < maze.NumberOfRows; ++row )
                for ( int column = 0; column < maze.NumberOfColumns; ++column )
                {
                    Assert.NotEmpty( maze[ row, column ].Links() );
                }
            }
        }
    }
}