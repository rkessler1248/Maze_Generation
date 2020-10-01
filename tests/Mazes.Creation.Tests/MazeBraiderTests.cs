using Mazes.Structures;
using Mazes.Tests.Utility;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class MazeBraiderTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_MazeBraider_Braid()
        {
            (float BraidPercent, int ExpectedCount)[] values = new[] { ( 0.0f, 8 ), ( 0.25f, 6 ), ( 0.5f, 4 ), ( 0.75f, 2 ), ( 1.0f, 0 ) };

            for ( int i = 0; i < values.Length; ++i )
            {
                Maze maze = TestMaze.Generate();
                MazeBraider braider = new MazeBraider();
                braider.Braid( maze, values[ i ].BraidPercent );
                Assert.True( values[ i ].ExpectedCount >= maze.DeadEnds().Count );
            }
        }
    }
}