using System.Collections;
using System.Collections.Generic;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms
{
    public static class Sidewinder
    {
        public static void GenerateSidewinderMaze<TCell, TCoordinates, TCollection>( this ISidewinderEnabledMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset();

            var run = new List<TCell>();
            maze.ForEachCell( coordinates =>
            {
                run.Add( maze[ coordinates ] );
                var shouldCloseOut = maze.CloseOutRun( coordinates, RandomNumberGenerator.NextInt( 0, 1 ) == 0 );

                if ( shouldCloseOut )
                {
                    var randomCell = run[ RandomNumberGenerator.NextInt( 0, run.Count - 1 ) ];
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