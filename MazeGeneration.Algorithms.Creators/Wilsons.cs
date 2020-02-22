using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using RNG = MazeGeneration.Utility.RandomNumberGenerator;

namespace MazeGeneration.Algorithms.Creators
{
    public static class Wilsons
    {
        public static void GenerateWilsonsMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset();
            
            var unvisitedCells = Initialize( maze );
            while ( unvisitedCells.Any() )
            {
                var path = PerformLoopErasedWalk<TCell, TCoordinates>( unvisitedCells );
                CarvePath<TCell, TCoordinates>( path, unvisitedCells );
            }
        }

        private static Dictionary<string, TCell> Initialize<TCell, TCoordinates, TCollection>( IMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : ICell<TCoordinates> 
            where TCoordinates : ICoordinates 
            where TCollection : ICollection
        {
            var unvisitedCells = new Dictionary<string, TCell>();
            maze.ForEachCell( cell => unvisitedCells.Add( cell.Id, cell ) );

            var first = unvisitedCells.Values.ElementAt( RNG.NextInt( 0, unvisitedCells.Count ) );
            unvisitedCells.Remove( first.Id );
            
            return unvisitedCells;
        }

        private static List<TCell> PerformLoopErasedWalk<TCell, TCoordinates>( Dictionary<string, TCell> unvisitedCells )
            where TCell : ICell<TCoordinates> 
            where TCoordinates : ICoordinates
        {
            var cell = unvisitedCells.Values.ElementAt( RNG.NextInt( 0, unvisitedCells.Count ) );
            var path = new List<TCell> { cell };
            while ( unvisitedCells.ContainsKey( cell.Id ) )
            {
                cell = ( TCell ) cell.Neighbors.Values.ElementAt( RNG.NextInt( 0, cell.Neighbors.Count ) );
                var pathIdx = path.IndexOf( cell );
                if ( pathIdx == -1 )
                {
                    path.Add( cell );
                }
                else
                {
                    path = path.Take( pathIdx + 1 ).ToList();
                }
            }

            return path;
        }
        
        private static void CarvePath<TCell, TCoordinates>( IList<TCell> path, IDictionary<string, TCell> unvisitedCells )
            where TCell : ICell<TCoordinates> 
            where TCoordinates : ICoordinates
        {
            for ( int cell = 0; cell < path.Count - 1; ++cell )
            {
                var first = path[ cell ];
                var second = path[ cell + 1 ];
                first.LinkTo( second );
                unvisitedCells.Remove( first.Id );
            }
        }
    }
}