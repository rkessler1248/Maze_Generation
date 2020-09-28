using Mazes.Tests.Utility;
using Xunit;

namespace Mazes.Structures.Tests
{
    public class DistancesTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Distances_CalculateDijkstra()
        {
            Maze maze = TestMaze.Generate();
            Distances distances = Distances.CalculateDijkstra( maze[ 0, 0 ] );

            Assert.Equal( 0, distances[ maze[ 0, 0 ] ] );
            
            Assert.Equal( 1, distances[ maze[ 0, 1 ] ] );

            Assert.Equal( 2, distances[ maze[ 0, 2 ] ] );
            Assert.Equal( 2, distances[ maze[ 1, 1 ] ] );

            Assert.Equal( 3, distances[ maze[ 0, 3 ] ] );
            Assert.Equal( 3, distances[ maze[ 1, 0 ] ] );
            Assert.Equal( 3, distances[ maze[ 1, 2 ] ] );
            Assert.Equal( 3, distances[ maze[ 2, 1 ] ] );

            Assert.Equal( 4, distances[ maze[ 0, 4 ] ] );
            Assert.Equal( 4, distances[ maze[ 2, 0 ] ] );
            Assert.Equal( 4, distances[ maze[ 3, 1 ] ] );

            Assert.Equal( 5, distances[ maze[ 1, 4 ] ] );
            Assert.Equal( 5, distances[ maze[ 3, 0 ] ] );
            Assert.Equal( 5, distances[ maze[ 3, 2 ] ] );

            Assert.Equal( 6, distances[ maze[ 2, 2 ] ] );
            Assert.Equal( 6, distances[ maze[ 2, 4 ] ] );
            Assert.Equal( 6, distances[ maze[ 3, 3 ] ] );
            Assert.Equal( 6, distances[ maze[ 4, 0 ] ] );

            Assert.Equal( 7, distances[ maze[ 2, 3 ] ] );
            Assert.Equal( 7, distances[ maze[ 3, 4 ] ] );
            Assert.Equal( 7, distances[ maze[ 4, 1 ] ] );
            Assert.Equal( 7, distances[ maze[ 4, 3 ] ] );

            Assert.Equal( 8, distances[ maze[ 1, 3 ] ] );
            Assert.Equal( 8, distances[ maze[ 4, 2 ] ] );
            Assert.Equal( 8, distances[ maze[ 4, 4 ] ] );
        }
    }
}