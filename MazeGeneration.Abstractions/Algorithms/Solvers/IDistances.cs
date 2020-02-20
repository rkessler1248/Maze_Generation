using System.Collections.Generic;

namespace MazeGeneration.Abstractions.Algorithms.Solvers
{
    public interface IDistances<TCell, TCoordinates> where TCell : ICell<TCoordinates>
                                                     where TCoordinates : ICoordinates
    {
        TCell Root { get; }
        IDictionary<string, int> Values { get; }
        IDictionary<string, TCell> Cells { get; }
        int this[ string cell ] { get; set; }
        IList<string> Keys();
        IDistances<TCell, TCoordinates> PathTo( TCell goal );
        (TCell MaxCell, int MaxDistance) Max();
    }
}