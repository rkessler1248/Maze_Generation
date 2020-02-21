using System.Collections;
using System.Collections.Generic;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Creators;
using RNG = MazeGeneration.Utility.RandomNumberGenerator;

namespace MazeGeneration.Algorithms.Creators
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
                var shouldCloseOut = maze.CloseOutRun( coordinates, RNG.NextInt( 0, 2 ) == 0 );

                if ( shouldCloseOut )
                {
                    var randomCell = run[ RNG.NextInt( 0, run.Count ) ];
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