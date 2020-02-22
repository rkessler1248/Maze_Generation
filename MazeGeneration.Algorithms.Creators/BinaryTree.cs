using System.Collections;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Creators;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms.Creators
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
                                    .Where( cell => cell.IsNotNull() )
                                    .ToList();
                if ( neighbors.Any() )
                {
                    maze[ coordinates ].LinkTo( neighbors.GetRandom() );
                }
            } );
        }
    }
}