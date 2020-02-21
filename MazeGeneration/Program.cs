using System;
using MazeGeneration.Algorithms;
using MazeGeneration.Algorithms.Creators;
using MazeGeneration.Models.Orthogonal;

namespace MazeGeneration
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            var maze = new OrthogonalMaze2D( 10, 10 );
            
            maze.GenerateBinaryTreeMaze();
            Console.Write( "Binary Tree Maze\n" );
            Console.WriteLine( maze.Print() );
            
            Console.Write( "Sidewinder Maze\n" );
            maze.GenerateSidewinderMaze();
            Console.WriteLine( maze.Print() );
            
            Console.Write( "Aldous-Broder Maze\n" );
            maze.GenerateAldousBroderMaze();
            Console.WriteLine( maze.Print() );
        }
    }
}