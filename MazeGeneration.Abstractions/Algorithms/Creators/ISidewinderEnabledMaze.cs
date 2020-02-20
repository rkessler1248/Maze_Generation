using System.Collections;

namespace MazeGeneration.Abstractions.Algorithms.Creators
{
    public interface ISidewinderEnabledMaze<TCell, TCoordinates, out TCollection> : IMaze<TCell, TCoordinates, TCollection> where TCell : ICell<TCoordinates>
                                                                                                                            where TCoordinates : ICoordinates
                                                                                                                            where TCollection : ICollection
    {
        bool CloseOutRun( TCoordinates coordinates, bool randomResult );
        void LinkCellToCloseOutNeighbor( TCell cell );
        void LinkCellToContinueNeighbor( TCell cell );
    }
}