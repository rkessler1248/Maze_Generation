using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility.Extensions;
using RNG = MazeGeneration.Utility.RandomNumberGenerator;

namespace MazeGeneration.Algorithms.Creators
{
    public static class Sidewinder
    {
        public static void GenerateSidewinderMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze,
                                                                                     IList<TCoordinates> mask = null )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            maze.Reset( mask: mask );

            var run = new List<TCell>();
            maze.ForEachCell( coordinates =>
            {
                run.Add( maze[ coordinates ] );
                var shouldCloseOut = maze.CloseOutRun( coordinates, RNG.NextInt( 0, 2 ) == 0 );

                if ( shouldCloseOut )
                {
                    var randomCell = run.GetRandom();
                    maze.LinkCellToCloseOutNeighbor( randomCell );
                    run.Clear();
                }
                else
                {
                    maze.LinkCellToContinueNeighbor( maze[ coordinates ] );
                }
            } );
        }

        private static bool CloseOutRun<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze,
                                                                           TCoordinates coordinates,
                                                                           bool randomResult )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            var closeOutRun = false;
            var neighborKey = new int[ coordinates.Dimensions ];
            for ( int dimension = 0; dimension < coordinates.Values.Count; ++dimension )
            {
                coordinates.Values.CopyTo( neighborKey, 0 );
                if ( BoundaryToCheckIsBelowCurrent( dimension ) )
                {
                    --neighborKey[ dimension ];
                }
                else
                {
                    ++neighborKey[ dimension ];
                }

                var neighbor = maze[ neighborKey ];
                var predicateToOr = neighbor.IsNull();

                if ( dimension == 0 )
                {
                    predicateToOr = !predicateToOr && randomResult;
                }

                closeOutRun = closeOutRun || predicateToOr;
            }

            return closeOutRun;
        }

        private static void LinkCellToContinueNeighbor<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze,
                                                                                          TCell cell )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            if ( !HasAContinueNeighborToLinkTo( maze, cell ) )
            {
                return;
            }

            var neighborKey = new int[ cell.Coordinates.Dimensions ];
            cell.Coordinates.Values.CopyTo( neighborKey, 0 );
            ++neighborKey[ 1 ];

            cell.LinkTo( maze[ neighborKey ] );
        }

        private static void LinkCellToCloseOutNeighbor<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze,
                                                                                          TCell cell )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            var dimensionsToCloseOut = Enumerable.Range( 0, maze.Dimensions )

                                                 // Dimension 1 is used for continuing, and should not be used in closing out the run.
                                                 .Where( dimension => dimension != 1 )
                                                 .ToArray();
            foreach ( var dimension in dimensionsToCloseOut )
            {
                if ( !HasACloseOutNeighborToLinkTo( maze, cell, dimension ) )
                {
                    continue;
                }

                var neighborKey = new int[ cell.Coordinates.Dimensions ];
                cell.Coordinates.Values.CopyTo( neighborKey, 0 );

                --neighborKey[ dimension ];
                cell.LinkTo( maze[ neighborKey ] );
            }
        }

        private static bool BoundaryToCheckIsBelowCurrent( int dimension )
        {
            /* NOTE
             * Like the Binary Tree, I am making an assumption here based on the
             * general texture of a 2D Sidewinder maze. If 0,0 is in the top left
             * corner, row is dimension 0, and column is dimension 1, then the
             * northern boundary would be below the current row numerically, and
             * the eastern boundary would be above the current column. If you move
             * to 3D, it would not make much of a difference if you went +z or -z.
             * The texture should still end up as uniquely that created by Sidewinder.
             * I chose to make the period every 4 dimensions, but there is no hard
             * and fast rule that says it must be so.
             */
            return dimension % 4 == 0;
        }

        private static bool HasAContinueNeighborToLinkTo<TCell, TCoordinates, TCollection>( IMaze<TCell, TCoordinates, TCollection> maze, TCell cell )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            var neighborKey = new int[ cell.Coordinates.Dimensions ];
            cell.Coordinates.Values.CopyTo( neighborKey, 0 );
            
            ++neighborKey[ 1 ];
            var neighbor = maze[ neighborKey ];
            
            return neighbor.IsNotNull();
        }

        private static bool HasACloseOutNeighborToLinkTo<TCell, TCoordinates, TCollection>( IMaze<TCell, TCoordinates, TCollection> maze, TCell cell, int dimension )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            var neighborKey = new int[ cell.Coordinates.Dimensions ];
            cell.Coordinates.Values.CopyTo( neighborKey, 0 );
            
            --neighborKey[ dimension ];
            var neighbor = maze[ neighborKey ];
            
            return neighbor.IsNotNull();
        }
    }
}