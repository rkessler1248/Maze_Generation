using System.Collections;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms
{
    public static class BinaryTree
    {
        public static void GenerateBinaryTreeMaze<TCell, TCoordinates, TCollection>( this IBinaryTreeEnabledMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset();

            maze.ForEachCell( coordinates =>
            {
                var neighbors = maze.NeighborsToChooseFrom( coordinates )
                                    .Where( cell => !( cell is null ) )
                                    .ToList();
                if ( neighbors.Count > 0 )
                {
                    maze[ coordinates ].LinkTo( neighbors[ RandomNumberGenerator.NextInt( 0, neighbors.Count - 1 ) ] );
                }
            } );
        }
    }
}