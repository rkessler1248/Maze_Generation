using System;
using Mazes.Creation.WallCarvers;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class MaskedMazeTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_MaskedMaze_Carve()
        {
            bool[ , ] mask = new bool[ , ]
            {
                { true, true, true },
                { true, true, false },
                { false, true, true }
            };

            const int certaintyIterations = 10_000;

            RecursiveBacktrackerWallCarver carver = new RecursiveBacktrackerWallCarver();

            for ( int i = 0; i < certaintyIterations; ++i )
            {
                MaskedMaze maze = new MaskedMaze( 3, 3, mask );

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
                    if ( maze[ row, column ] is null )
                    {
                        continue;
                    }

                    Assert.NotEmpty( maze[ row, column ].Links );
                }
            }
        }
    }
}