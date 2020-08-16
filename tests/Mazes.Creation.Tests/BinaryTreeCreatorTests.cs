using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests {
    public class BinaryTreeCreatorTests {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_BinaryTreeCreator_2D_OrthogonalMaze() {
            Maze maze = new Maze( 10, 10 );
            BinaryTreeCreator creator = new BinaryTreeCreator( maze );

            creator.Carve();
            int row = maze.Rows - 1;
            int column;
            for ( column = 0; column < maze.Columns - 1; ++column ) {
                Cell current = maze[ row, column ];
                Cell next = maze[ row, column + 1 ];

                Assert.True( current.IsLinkedTo( next ) );
                Assert.True( next.IsLinkedTo( current ) );
            }

            column = maze.Columns - 1;
            for ( row = 0; row < maze.Rows - 1; ++row ) {
                Cell current = maze[ row, column ];
                Cell next = maze[ row + 1, column ];

                Assert.True( current.IsLinkedTo( next ) );
                Assert.True( next.IsLinkedTo( current ) );
            }
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_BinaryTreeCreator_2D_OrthogonalMaze_ChooseLink() {
            Maze maze = new Maze( 3, 3 );
            BinaryTreeCreator creator = new BinaryTreeCreator( maze );

            Cell link = creator.ChooseLink( maze[ 0, 0 ] );
            Assert.Contains( link, new[] { maze[ 1, 0 ], maze[ 0, 1 ] } );

            link = creator.ChooseLink( maze[ 0, maze.Columns - 1 ] );
            Assert.Equal( link, maze[ 1, maze.Columns - 1 ] );

            link = creator.ChooseLink( maze[ maze.Rows - 1, 1 ] );
            Assert.Equal( link, maze[ maze.Rows - 1, 2 ] );
        }
    }
}