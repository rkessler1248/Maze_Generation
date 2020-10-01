using Xunit;

namespace Mazes.Structures.Tests
{
    public class MaskedMazeTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_MaskedMaze_Construction()
        {
            bool[ , ] mask = new bool[ , ]
            {
                { true, true, true },
                { true, true, false },
                { false, true, true }
            };

            MaskedMaze maze = new MaskedMaze( 3, 3, mask );
            Assert.Equal( 7, maze.Size );

            Assert.Null( maze[ 1, 2 ] );
            Assert.Null( maze[ 2, 0 ] );

            Assert.Equal( 1, maze[ 0, 2 ].Neighbors.Count );
            Assert.Equal( 1, maze[ 2, 2 ].Neighbors.Count );

            Assert.Equal( 2, maze[ 1, 0 ].Neighbors.Count );
            Assert.Equal( 2, maze[ 2, 1 ].Neighbors.Count );

            Assert.Equal( 3, maze[ 1, 1 ].Neighbors.Count );
        }
    }
}