using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Solvers;
using MazeGeneration.Models.Algorithms.Solvers;

namespace MazeGeneration.Extensions.Cells
{
    public static class DijkstraCellExtensions
    {
        public static IDistances<TCell, TCoordinates> Distances<TCell, TCoordinates>( this TCell cell )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
        {
            var distances = new Distances<TCell, TCoordinates>( cell );
            var frontier = new List<TCell> { cell };

            while ( frontier.Any() )
            {
                var newFrontier = new List<TCell>();
                foreach ( var frontierCell in frontier )
                {
                    foreach ( var link in frontierCell.Links )
                    {
                        if ( distances[ link.Value.Id ] != -1 )
                        {
                            continue;
                        }

                        distances[ link.Value.Id ] = distances[ frontierCell.Id ] + 1;
                        newFrontier.Add( ( TCell ) link.Value );
                    }
                }

                frontier = newFrontier;
            }

            return distances;
        }
    }
}