using System;
using System.Collections.Generic;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class BinaryTreeCreatorTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_BinaryTreeCreator_ChooseLink_NorthernBorder()
        {
            Maze maze = new Maze( 3, 3 );
            BinaryTreeCreator creator = new BinaryTreeCreator();

            Cell link1 = creator.ChooseLink( maze, 2, 0 );
            Assert.Equal( 2, link1.Row );
            Assert.Equal( 1, link1.Column );

            Cell link2 = creator.ChooseLink( maze, 2, 1 );
            Assert.Equal( 2, link2.Row );
            Assert.Equal( 2, link2.Column );

            Cell link3 = creator.ChooseLink( maze, 2, 2 );
            Assert.Null( link3 );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_BinaryTreeCreator_ChooseLink_EasternBorder()
        {
            Maze maze = new Maze( 3, 3 );
            BinaryTreeCreator creator = new BinaryTreeCreator();

            Cell link1 = creator.ChooseLink( maze, 0, 2 );
            Assert.Equal( 1, link1.Row );
            Assert.Equal( 2, link1.Column );

            Cell link2 = creator.ChooseLink( maze, 1, 2 );
            Assert.Equal( 2, link2.Row );
            Assert.Equal( 2, link2.Column );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_BinaryTreeCreator_ChooseLink_Remaining()
        {
            const int certaintyIterations = 10_000;

            Maze maze = new Maze( 3, 3 );
            BinaryTreeCreator creator = new BinaryTreeCreator();

            for ( int i = 0; i < certaintyIterations; ++i )
            {
                Cell link1 = creator.ChooseLink( maze, 0, 0 );
                Assert.True( new HashSet<Cell> { maze[ 0, 1 ], maze[ 1, 0 ] }.Contains( link1 ) );

                Cell link2 = creator.ChooseLink( maze, 1, 1 );
                Assert.True( new HashSet<Cell> { maze[ 2, 1 ], maze[ 1, 2 ] }.Contains( link2 ) );
                Assert.False( new HashSet<Cell> { maze[ 1, 0 ], maze[ 0, 1 ] }.Contains( link2 ) );
            }
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_BinaryTreeCreator_Carve()
        {
            const int certaintyIterations = 10_000;

            BinaryTreeCreator creator = new BinaryTreeCreator();

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