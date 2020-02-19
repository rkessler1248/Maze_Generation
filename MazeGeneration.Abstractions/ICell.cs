using System.Collections.Generic;

namespace MazeGeneration.Abstractions
{
    public interface ICell<TCoordinates> where TCoordinates : ICoordinates
    {
        string Id { get; }
        TCoordinates Coordinates { get; }
        IDictionary<string, ICell<TCoordinates>> Neighbors { get; }
        IDictionary<string, ICell<TCoordinates>> Links { get; }
        Tessellation Tessellation { get; }

        bool IsNeighbor( ICell<TCoordinates> cell );
        void AddNeighbor( ICell<TCoordinates> cell, bool addBidirectionally = true );
        void RemoveNeighbor( ICell<TCoordinates> cell, bool removeBidirectionally = true );

        bool IsLinkedTo( ICell<TCoordinates> cell );
        void LinkTo( ICell<TCoordinates> cell, bool linkBidirectionally = true );
        void UnlinkFrom( ICell<TCoordinates> cell, bool unlinkBidirectionally = true );
    }
}