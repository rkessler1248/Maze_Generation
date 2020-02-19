using System.Collections.Generic;

namespace MazeGeneration.Abstractions
{
    public interface ICoordinates
    {
        int Dimensions { get; }
        IList<int> Values { get; }
        int this[ int dimension ] { get; set; }
    }
}