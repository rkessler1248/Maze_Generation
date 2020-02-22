using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms.Creators
{
    public static class RecursiveBacktracker
    {
        public static void GenerateRecursiveBacktrackerMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze, IList<TCoordinates> mask = null )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset( mask: mask );

            var stack = new Stack<TCell>();
            stack.Push( maze.GetRandomCell() );

            while ( stack.Any() )
            {
                var current = stack.Peek();
                var unvisitedNeighbors = current.Neighbors
                                                .Values
                                                .Where( neighbor => neighbor.Links.IsEmpty() )
                                                .ToList();
                
                if ( unvisitedNeighbors.IsEmpty() )
                {
                    stack.Pop();
                }
                else
                {
                    var neighbor = ( TCell ) unvisitedNeighbors.GetRandom();
                    current.LinkTo( neighbor );
                    stack.Push( neighbor );
                }
            }
        }
    }
}