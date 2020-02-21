using System.Collections;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms.Creators
{
    public static class AldousBroder
    {
        public static void GenerateAldousBroderMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset();

            int unvisitedCellCount = maze.Size - 1;
            var current = maze.GetRandomCell();
            while ( unvisitedCellCount > 0 )
            {
                var neighbors = current.Neighbors.Values.ToList();
                var chosenNeighborIdx = RandomNumberGenerator.NextInt( 0, neighbors.Count - 1 );
                var chosenNeighbor = neighbors[ chosenNeighborIdx ];

                if ( chosenNeighbor.Links.Count == 0 )
                {
                    current.LinkTo( chosenNeighbor );
                    --unvisitedCellCount;
                }

                current = ( TCell ) chosenNeighbor;
            }
        }
    }
}