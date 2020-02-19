using System.Collections.Generic;
using MazeGeneration.Abstractions;

namespace MazeGeneration.Models
{
    public abstract class Cell<TCoordinates> : ICell<TCoordinates> where TCoordinates : ICoordinates
    {
        public string Id => string.Join( "-", Coordinates.Values );

        public TCoordinates Coordinates { get; protected set; }
        public IDictionary<string, ICell<TCoordinates>> Neighbors { get; protected set; }
        public IDictionary<string, ICell<TCoordinates>> Links { get; protected set; }
        public Tessellation Tessellation { get; protected set; }

        public bool IsNeighbor( ICell<TCoordinates> cell )
        {
            return !( cell is null ) && Neighbors.ContainsKey( cell.Id );
        }

        public virtual void AddNeighbor( ICell<TCoordinates> cell, bool addBidirectionally = true )
        {
            if ( cell is null || cell == this || IsNeighbor( cell ) )
            {
                return;
            }

            Neighbors[ cell.Id ] = cell;

            if ( !addBidirectionally )
            {
                return;
            }

            cell.AddNeighbor( this );
        }

        public virtual void RemoveNeighbor( ICell<TCoordinates> cell, bool removeBidirectionally = true )
        {
            if ( cell is null || cell == this || !IsNeighbor( cell ) )
            {
                return;
            }

            UnlinkFrom( cell );
            Neighbors.Remove( cell.Id );

            if ( removeBidirectionally )
            {
                cell.RemoveNeighbor( this );
            }
        }

        public virtual bool IsLinkedTo( ICell<TCoordinates> cell )
        {
            return !( cell is null ) && Links.ContainsKey( cell.Id );
        }

        public virtual void LinkTo( ICell<TCoordinates> cell, bool linkBidirectionally = true )
        {
            if ( cell is null || cell == this || IsLinkedTo( cell ) )
            {
                return;
            }

            Links[ cell.Id ] = cell;

            if ( !linkBidirectionally )
            {
                return;
            }

            cell.LinkTo( this );
        }

        public virtual void UnlinkFrom( ICell<TCoordinates> cell, bool unlinkBidirectionally = true )
        {
            if ( cell is null || cell == this || !IsLinkedTo( cell ) )
            {
                return;
            }

            Links.Remove( cell.Id );

            if ( unlinkBidirectionally )
            {
                cell.UnlinkFrom( this );
            }
        }
    }
}