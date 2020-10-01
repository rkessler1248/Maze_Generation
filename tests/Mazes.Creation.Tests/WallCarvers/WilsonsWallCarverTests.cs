using System;
using System.Collections.Generic;
using Mazes.Creation.WallCarvers;
using Mazes.Structures;
using Xunit;

namespace Mazes.Creation.Tests.WallCarvers
{
    public class WilsonsWallCarverTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_WilsonsWallCarver_EraseLoop_NoLoop()
        {
            Maze maze = new Maze( 3, 3 );
            WilsonsWallCarver carver = new WilsonsWallCarver();

            Stack<Cell> path = new Stack<Cell>();
            path.Push( maze[ 0, 0 ] );
            path.Push( maze[ 0, 1 ] );

            carver.EraseLoop( path );

            Assert.Equal( maze[ 0, 1 ], path.Pop() );
            Assert.Equal( maze[ 0, 0 ], path.Pop() );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_WilsonsWallCarver_EraseLoop_Loop()
        {
            Maze maze = new Maze( 3, 3 );
            WilsonsWallCarver carver = new WilsonsWallCarver();

            Stack<Cell> path = new Stack<Cell>();

            path.Push( maze[ 0, 0 ] );
            path.Push( maze[ 0, 1 ] );
            path.Push( maze[ 0, 2 ] );
            path.Push( maze[ 1, 2 ] );
            path.Push( maze[ 1, 1 ] );
            path.Push( maze[ 0, 1 ] );

            carver.EraseLoop( path );

            Assert.Equal( maze[ 0, 1 ], path.Pop() );
            Assert.Equal( maze[ 0, 0 ], path.Pop() );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_WilsonsWallCarver_IntersectedVisited_True()
        {
            Maze maze = new Maze( 3, 3 );
            WilsonsWallCarver carver = new WilsonsWallCarver();

            Stack<Cell> path = new Stack<Cell>();
            path.Push( maze[ 0, 0 ] );
            path.Push( maze[ 0, 1 ] );

            ISet<Cell> visited = new HashSet<Cell> { maze[ 0, 1 ] };
            Assert.True( carver.IntersectedVisited( visited, path ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_WilsonsWallCarver_IntersectedVisited_False()
        {
            Maze maze = new Maze( 3, 3 );
            WilsonsWallCarver carver = new WilsonsWallCarver();

            Stack<Cell> path = new Stack<Cell>();
            path.Push( maze[ 0, 0 ] );
            path.Push( maze[ 0, 1 ] );

            ISet<Cell> visited = new HashSet<Cell> { maze[ 0, 2 ] };
            Assert.False( carver.IntersectedVisited( visited, path ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_WilsonsWallCarver_CarvePath()
        {
            Maze maze = new Maze( 3, 3 );
            WilsonsWallCarver carver = new WilsonsWallCarver();

            Stack<Cell> path = new Stack<Cell>();
            path.Push( maze[ 0, 0 ] );
            path.Push( maze[ 0, 1 ] );
            path.Push( maze[ 0, 2 ] );

            ISet<Cell> visited = new HashSet<Cell> { maze[ 0, 2 ] };
            ISet<Cell> unvisited = new HashSet<Cell>
            {
                maze[ 0, 0 ],
                maze[ 0, 1 ],
                maze[ 1, 0 ],
                maze[ 1, 1 ],
                maze[ 1, 2 ],
                maze[ 2, 0 ],
                maze[ 2, 1 ],
                maze[ 2, 2 ]
            };
            carver.CarvePath( visited, unvisited, path );

            Assert.Empty( path );
            Assert.Equal( 3, visited.Count );
            Assert.Contains( maze[ 0, 0 ], visited );
            Assert.Contains( maze[ 0, 1 ], visited );
            Assert.Contains( maze[ 0, 2 ], visited );

            Assert.Equal( 6, unvisited.Count );
            Assert.DoesNotContain( maze[ 0, 0 ], unvisited );
            Assert.DoesNotContain( maze[ 0, 1 ], unvisited );
            Assert.DoesNotContain( maze[ 0, 2 ], unvisited );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_WilsonsWallCarver_Carve()
        {
            const int certaintyIterations = 10_000;

            WilsonsWallCarver carver = new WilsonsWallCarver();

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