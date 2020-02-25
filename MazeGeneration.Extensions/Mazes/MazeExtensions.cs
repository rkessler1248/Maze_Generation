using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility.Extensions;

namespace MazeGeneration.Extensions.Mazes
{
    public static class MazeExtensions
    {
        public static TCell GetRandomCell<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            return maze.Cells
                       .Cast<TCell>()
                       .Where( cell => cell.IsNotNull() )
                       .GetRandom();
        }

        public static IList<TCell> DeadEnds<TCell, TCoordinates, TCollection>( this IMaze<TCell, TCoordinates, TCollection> maze )
            where TCell : class, ICell<TCoordinates>
            where TCoordinates : class, ICoordinates
            where TCollection : class, ICollection
        {
            return maze.Cells
                       .Cast<TCell>()
                       .Where( cell => cell.IsNotNull() && cell.Links.Count == 1 )
                       .ToList();
        }
    }
}