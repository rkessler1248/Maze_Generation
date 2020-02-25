using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Extensions;
using MazeGeneration.Extensions.Mazes;
using MazeGeneration.Utility.Extensions;

namespace MazeGeneration.Algorithms.Creators
{
    public static class AldousBroder
    {
        public static void GenerateAldousBroderMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze, IList<TCoordinates> mask = null )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            maze.Reset( mask: mask );

            var unvisitedCellCount = maze.Size - 1;
            var current = maze.GetRandomCell();
            while ( unvisitedCellCount > 0 )
            {
                var neighbors = current.Neighbors.Values.ToList();
                var chosenNeighbor = neighbors.GetRandom();

                if ( chosenNeighbor.Links.IsEmpty() )
                {
                    current.LinkTo( chosenNeighbor );
                    --unvisitedCellCount;
                }

                current = ( TCell ) chosenNeighbor;
            }
        }
    }
}