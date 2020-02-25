using System.Collections.Generic;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility.Extensions;

namespace MazeGeneration.Models
{
    public abstract class Cell<TCoordinates> : ICell<TCoordinates>
        where TCoordinates : class, ICoordinates
    {
        public const string IdSeparator = "-";
        public string Id => string.Join( IdSeparator, Coordinates.Values );

        public TCoordinates Coordinates { get; protected set; }
        public IDictionary<string, ICell<TCoordinates>> Neighbors { get; protected set; }
        public IDictionary<string, ICell<TCoordinates>> Links { get; protected set; }
        public Tessellation Tessellation { get; protected set; }

        public bool IsNeighbor( ICell<TCoordinates> cell )
        {
            return cell.IsNotNull() && Neighbors.ContainsKey( cell.Id );
        }

        public virtual void AddNeighbor( ICell<TCoordinates> cell, bool addBidirectionally = true )
        {
            if ( cell.IsNull() || cell == this || IsNeighbor( cell ) )
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
            if ( cell.IsNull() || cell == this || !IsNeighbor( cell ) )
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
            return cell.IsNotNull() && Links.ContainsKey( cell.Id );
        }

        public virtual void LinkTo( ICell<TCoordinates> cell, bool linkBidirectionally = true )
        {
            if ( cell.IsNull() || cell == this || IsLinkedTo( cell ) )
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
            if ( cell.IsNull() || cell == this || !IsLinkedTo( cell ) )
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