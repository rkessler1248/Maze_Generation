using System.Collections;
using System.Collections.Generic;

namespace MazeGeneration.Abstractions.Algorithms
{
    public interface IBinaryTreeEnabledMaze<TCell, TCoordinates, out TCollection> : IMaze<TCell, TCoordinates, TCollection> where TCell : ICell<TCoordinates>
                                                                                                                            where TCoordinates : ICoordinates
                                                                                                                            where TCollection : ICollection
    {
        IList<TCell> NeighborsToChooseFrom( TCoordinates coordinates );
    }
}