using System;
using System.Collections;
using System.Collections.Generic;

namespace MazeGeneration.Abstractions
{
    public interface IMaze<TCell, TCoordinates, out TCollection> where TCell : ICell<TCoordinates>
                                                                 where TCoordinates : ICoordinates
                                                                 where TCollection : ICollection
    {
        int Dimensions { get; }
        IList<int> DimensionSizes { get; }

        TCollection Cells { get; }
        TCell this[ params int[] keys ] { get; set; }
        TCell this[ TCoordinates coordinates ] { get; set; }
        TCell this[ string id ] { get; set; }
        void Reset( bool linkAllCells = false );

        void ForEachCell( Action<TCoordinates> action );
        string Print();
    }
}