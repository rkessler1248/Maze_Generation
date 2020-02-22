using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Creators;
using MazeGeneration.Utility;
using RNG = MazeGeneration.Utility.RandomNumberGenerator;

namespace MazeGeneration.Algorithms.Creators
{
    public static class Sidewinder
    {
        public static void GenerateSidewinderMaze<TCell, TCoordinates, TCollection>( this ISidewinderEnabledMaze<TCell, TCoordinates, TCollection> maze, IList<TCoordinates> mask = null )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
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
    }
}