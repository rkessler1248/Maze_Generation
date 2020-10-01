using Mazes.Structures.Utility;
using Mazes.Tests.Utility;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class MazeDisplayerTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_MazeDisplayer_AsString()
        {
            MazeDisplayer displayer = new MazeDisplayer();
            string printed = displayer.AsString( TestMaze.Generate() );
            Assert.True( !string.IsNullOrWhiteSpace( printed ) );
        }
    }
}