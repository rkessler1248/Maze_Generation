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
        int Size { get; }

        TCollection Cells { get; }
        TCell this[ params int[] keys ] { get; set; }
        TCell this[ TCoordinates coordinates ] { get; set; }
        TCell this[ string id ] { get; set; }
        void Reset( bool linkAllCells = false, IList<TCoordinates> mask = null );

        void ForEachCell( Action<TCoordinates> action );
        void ForEachCell( Action<TCell> action );

        TCell GetRandomCell();
        IList<TCell> DeadEnds();
        string Print();
    }
}