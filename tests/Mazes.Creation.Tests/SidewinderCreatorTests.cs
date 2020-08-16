using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests {
    public class SidewinderCreatorTests {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_SidewinderCreator_2D_OrthogonalMaze() {
            Maze maze = new Maze( 3, 3 );
            SidewinderCreator creator = new SidewinderCreator( maze );

            creator.Carve();
        }
    }
}