using System;
using System.Collections.Generic;
using MazeGeneration.Abstractions.Orthogonal;
using MazeGeneration.Algorithms.Creators;
using MazeGeneration.Models.Orthogonal;

namespace MazeGeneration
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            var maze = new OrthogonalMaze2D( 10, 10 );

            var testMask = new List<IOrthogonalCoordinates2D>
            {
                // H
                new OrthogonalCoordinates2D { Row = 1, Column = 0 },
                new OrthogonalCoordinates2D { Row = 1, Column = 2 },
                new OrthogonalCoordinates2D { Row = 2, Column = 0 },
                new OrthogonalCoordinates2D { Row = 2, Column = 2 },
                new OrthogonalCoordinates2D { Row = 3, Column = 0 },
                new OrthogonalCoordinates2D { Row = 3, Column = 1 },
                new OrthogonalCoordinates2D { Row = 3, Column = 2 },
                new OrthogonalCoordinates2D { Row = 4, Column = 0 },
                new OrthogonalCoordinates2D { Row = 4, Column = 2 },
                new OrthogonalCoordinates2D { Row = 5, Column = 0 },
                new OrthogonalCoordinates2D { Row = 5, Column = 2 },

                // I
                new OrthogonalCoordinates2D { Row = 1, Column = 4 },
                new OrthogonalCoordinates2D { Row = 1, Column = 5 },
                new OrthogonalCoordinates2D { Row = 1, Column = 6 },
                new OrthogonalCoordinates2D { Row = 2, Column = 5 },
                new OrthogonalCoordinates2D { Row = 3, Column = 5 },
                new OrthogonalCoordinates2D { Row = 4, Column = 5 },
                new OrthogonalCoordinates2D { Row = 5, Column = 4 },
                new OrthogonalCoordinates2D { Row = 5, Column = 5 },
                new OrthogonalCoordinates2D { Row = 5, Column = 6 },

                // !
                new OrthogonalCoordinates2D { Row = 1, Column = 8 },
                new OrthogonalCoordinates2D { Row = 2, Column = 8 },
                new OrthogonalCoordinates2D { Row = 3, Column = 8 },
                new OrthogonalCoordinates2D { Row = 5, Column = 8 }
            };

            // TODO: Using masks on Binary Tree and Sidewinder have a tendency to produce orphan cells ( e.g. cells that have no links ),
            // TODO: or areas that are unconnected from the remainder of the maze. This needs to be corrected.
            maze.GenerateBinaryTreeMaze();
            Console.Write( "Binary Tree Maze\n" );
            Console.WriteLine( maze.Print() );

            Console.Write( "Sidewinder Maze\n" );
            maze.GenerateSidewinderMaze();
            Console.WriteLine( maze.Print() );

            Console.Write( "Aldous-Broder Maze\n" );
            maze.GenerateAldousBroderMaze();
            Console.WriteLine( maze.Print() );

            Console.Write( "Wilson's Maze\n" );
            maze.GenerateWilsonsMaze();
            Console.WriteLine( maze.Print() );

            Console.Write( "Hunt And Kill Maze\n" );
            maze.GenerateHuntAndKillMaze();
            Console.WriteLine( maze.Print() );

            Console.Write( "Recursive Backtracker Maze\n" );
            maze.GenerateRecursiveBacktrackerMaze( testMask );
            Console.WriteLine( maze.Print() );
        }
    }
}