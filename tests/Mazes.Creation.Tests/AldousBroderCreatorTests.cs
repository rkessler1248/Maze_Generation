using System;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class AldousBroderCreatorTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_AldousBroderCreator_LinkToCurrent()
        {
            AldousBroderCreator creator = new AldousBroderCreator();

            Cell cell1 = new Cell( 0, 0 );
            Assert.True( creator.LinkToCurrent( cell1 ) );

            Cell cell2 = new Cell( 0, 1 );
            cell1.LinkTo( cell2 );
            
            Assert.False( creator.LinkToCurrent( cell1 ) );
            Assert.False( creator.LinkToCurrent( cell2 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_AldousBroderCreator_Carve()
        {
            const int certaintyIterations = 10_000;

            AldousBroderCreator creator = new AldousBroderCreator();

            for ( int i = 0; i < certaintyIterations; ++i )
            {
                Maze maze = new Maze( 3, 3 );

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