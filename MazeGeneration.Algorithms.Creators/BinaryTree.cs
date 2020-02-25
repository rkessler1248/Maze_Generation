using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Extensions;
using MazeGeneration.Extensions.Mazes;
using MazeGeneration.Utility.Extensions;

namespace MazeGeneration.Algorithms.Creators
{
    public static class BinaryTree
    {
        public static void GenerateBinaryTreeMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze,
                                                                                     IList<TCoordinates> mask = null )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            maze.Reset( mask: mask );

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

        private static IList<TCell> NeighborsToChooseFrom<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze, TCoordinates coordinates )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            var neighbors = new List<TCell>();
            for ( int dimension = 0; dimension < coordinates.Values.Count; ++dimension )
            {
                var key = new int[ coordinates.Dimensions ];
                coordinates.Values.CopyTo( key, 0 );
                key[ dimension ] += KeyDimensionModifier( dimension );
                neighbors.Add( maze[ key ] );
            }

            return neighbors;
        }

        private static int KeyDimensionModifier( int dimension )
        {
            /* NOTE
             * I am making an assumption here based on the general texture of a 2D
             * Binary Tree maze. If 0,0 is in the top left corner, row is dimension 0,
             * and column is dimension 1, then to get the northern neighbor, you would
             * need to add -1 to the current row, and to get the eastern neighbor,
             * you would need to add 1 to the current column. If you move to 3D, it
             * would not make much of a difference if you went +z or -z. The texture
             * should still end up as uniquely that created by Binary Tree. I chose to
             * make the period every 4 dimensions, but there is no hard and fast rule
             * that says it must be so.
             */
            return dimension % 4 == 0 ? -1 : 1;
        }
    }
}