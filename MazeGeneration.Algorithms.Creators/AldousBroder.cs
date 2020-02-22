using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms.Creators
{
    public static class AldousBroder
    {
        public static void GenerateAldousBroderMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze, IList<TCoordinates> mask = null )
            where TCell : ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset();
            if ( mask.IsNotNull() )
            {
                maze.ApplyMask( mask );
            }

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