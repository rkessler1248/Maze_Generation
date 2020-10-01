using System;
using System.Collections.Generic;
using Mazes.Creation.WallCarvers;
using Mazes.Structures;
using Mazes.Tests.Utility;
using Xunit;

namespace Mazes.Creation.Tests
{
    public class RecursiveBacktrackerWallCarverTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_RecursiveBacktrackerWallCarver_Backtrack_UnvisitedCellsAvailable()
        {
            Maze maze = new Maze( 3, 3 );
            RecursiveBacktrackerWallCarver carver = new RecursiveBacktrackerWallCarver();

            maze[ 0, 0 ].LinkTo( maze[ 0, 1 ] );
            maze[ 0, 1 ].LinkTo( maze[ 1, 1 ] );
            maze[ 1, 1 ].LinkTo( maze[ 2, 1 ] );
            maze[ 2, 1 ].LinkTo( maze[ 2, 0 ] );
            maze[ 2, 0 ].LinkTo( maze[ 1, 0 ] );

            Stack<Cell> path = new Stack<Cell>();

            path.Push( maze[ 0, 0 ] );
            path.Push( maze[ 0, 1 ] );
            path.Push( maze[ 1, 1 ] );
            path.Push( maze[ 2, 1 ] );
            path.Push( maze[ 2, 0 ] );
            path.Push( maze[ 1, 0 ] );

            carver.Backtrack( path );

            Assert.Equal( maze[ 2, 2 ], path.Peek() );
            Assert.True( maze[ 2, 1 ].IsLinkedTo( maze[ 2, 2 ] ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_RecursiveBacktrackerWallCarver_Backtrack_UnvisitedCellsUnavailable()
        {
            Maze maze = TestMaze.Generate();
            RecursiveBacktrackerWallCarver carver = new RecursiveBacktrackerWallCarver();
            
            Stack<Cell> path = Distances.CalculateDijkstra( maze[ 0, 0 ] ).ShortestPath( maze[ 4, 4 ] );
            carver.Backtrack( path );
            
            Assert.Empty( path );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_RecursiveBacktrackerWallCarver_Carve()
        {
            const int certaintyIterations = 10_000;

            RecursiveBacktrackerWallCarver carver = new RecursiveBacktrackerWallCarver();

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