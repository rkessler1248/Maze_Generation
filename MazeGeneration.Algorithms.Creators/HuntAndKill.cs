using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility;

namespace MazeGeneration.Algorithms.Creators
{
    public static class HuntAndKill
    {
        public static void GenerateHuntAndKillMaze<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze, IList<TCoordinates> mask = null )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.Reset();
            if ( mask.IsNotNull() )
            {
                maze.ApplyMask( mask );
            }

            var current = maze.GetRandomCell();
            while ( current.IsNotNull() )
            {
                current = Kill<TCell, TCoordinates>( current );
                current = Hunt( maze, current );
            }
        }

        private static TCell Kill<TCell, TCoordinates>( TCell current )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
        {
            var unvisitedNeighbors = current.Neighbors
                                            .Values
                                            .Where( neighbor => neighbor.Links.IsEmpty() )
                                            .ToList();

            if ( unvisitedNeighbors.Any() )
            {
                var neighbor = unvisitedNeighbors.GetRandom();
                current.LinkTo( neighbor );
                current = ( TCell ) neighbor;
            }
            else
            {
                current = null;
            }

            return current;
        }

        private static TCell Hunt<TCell, TCoordinates, TCollection>( IMaze<TCell, TCoordinates, TCollection> maze, TCell current )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : ICoordinates
            where TCollection : ICollection
        {
            maze.ForEachCell( cell =>
            {
                var visitedNeighbors = cell.Neighbors
                                           .Values
                                           .Where( neighbor => neighbor.Links.Count > 0 )
                                           .ToList();

                if ( cell.Links.IsEmpty() && visitedNeighbors.Any() )
                {
                    current = cell;
                    var neighbor = visitedNeighbors.GetRandom();
                    current.LinkTo( neighbor );
                }
            } );
            return current;
        }
    }
}