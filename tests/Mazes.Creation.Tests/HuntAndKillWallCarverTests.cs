using System;
using Mazes.Creation.WallCarvers;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class HuntAndKillWallCarverTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_HuntAndKillWallCarver_Hunt_Visited()
        {
            Maze maze = new Maze( 3, 3 );
            HuntAndKillWallCarver carver = new HuntAndKillWallCarver();

            maze[ 0, 0 ].LinkTo( maze[ 1, 0 ] );


            for ( int row = 0; row < maze.NumberOfRows; ++row )
            for ( int column = 0; column < maze.NumberOfColumns; ++column )
            {
                if ( 0 == column && ( 0 == row || 1 == row ) )
                {
                    Assert.True( carver.Visited( maze[ row, column ] ) );
                }
                else
                {
                    Assert.False( carver.Visited( maze[ row, column ] ) );
                }
            }
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_HuntAndKillWallCarver_Hunt_ViableCandidate()
        {
            Maze maze = new Maze( 3, 3 );
            HuntAndKillWallCarver carver = new HuntAndKillWallCarver();

            maze[ 0, 0 ].LinkTo( maze[ 1, 0 ] );
            maze[ 1, 0 ].LinkTo( maze[ 1, 1 ] );
            maze[ 1, 1 ].LinkTo( maze[ 0, 1 ] );

            Assert.Equal( maze[ 2, 1 ], carver.Hunt( maze ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_HuntAndKillWallCarver_Hunt_NoViableCandidate()
        {
            Maze maze = new Maze( 3, 3 );
            HuntAndKillWallCarver carver = new HuntAndKillWallCarver();

            maze[ 0, 0 ].LinkTo( maze[ 0, 1 ] );
            maze[ 0, 1 ].LinkTo( maze[ 0, 2 ] );

            maze[ 1, 0 ].LinkTo( maze[ 1, 1 ] );
            maze[ 1, 1 ].LinkTo( maze[ 1, 2 ] );

            maze[ 2, 0 ].LinkTo( maze[ 2, 1 ] );
            maze[ 2, 1 ].LinkTo( maze[ 2, 2 ] );

            Assert.Null( carver.Hunt( maze ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_HuntAndKillWallCarver_Carve()
        {
            const int certaintyIterations = 10_000;

            HuntAndKillWallCarver carver = new HuntAndKillWallCarver();

            for ( int i = 0; i < certaintyIterations; ++i )
            {
                Maze maze = new Maze( 5, 5 );

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
                    Assert.NotEmpty( maze[ row, column ].Links );
                }
            }
        }
    }
}