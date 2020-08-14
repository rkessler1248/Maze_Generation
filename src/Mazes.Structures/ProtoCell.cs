using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazes.Structures
{
    public class ProtoCell
    {
        public string Id => $"{Row.ToString()}-{Column.ToString()}";
        public readonly int Row;
        public readonly int Column;
        public readonly IDictionary<string, ProtoCell> Neighbors = new Dictionary<string, ProtoCell>();
        public readonly IDictionary<string, ProtoCell> Links = new Dictionary<string, ProtoCell>();

        public ProtoCell( int row, int column )
        {
            Row = row;
            Column = column;
        }

        public void LinkTo( ProtoCell link )
        {
            if ( this != link && CanLink( link ) && !IsLinkedTo( link ) )
            {
                Links[ link.Id ] = link;
                link.LinkTo( this );
            }
        }

        public void MakeNeighbors( in ProtoCell neighbor )
        {
            if ( this != neighbor && CanLink( neighbor ) && !IsNeighborOf( neighbor ) )
            {
                Neighbors[ neighbor.Id ] = neighbor;
                neighbor.MakeNeighbors( this );
            }
        }

        public bool IsLinkedTo( in ProtoCell cell )
        {
            return Links.ContainsKey( cell.Id );
        }

        public bool IsNeighborOf( in ProtoCell cell )
        {
            return Neighbors.ContainsKey( cell.Id );
        }

        public Cell Complete()
        {
            Cell[] links = ( from link in Links.Values select link.Complete() ).ToArray();
            Cell[] neighbors = ( from neighbor in Neighbors.Values select neighbor.Complete() ).ToArray();
            
            return new Cell( Row, Column, links, neighbors );
        }

        public bool Equals( in ProtoCell other )
        {
            return !CanLink( other )
                   && Equals( Links, other.Links )
                   && Equals( Links, other.Links );
        }

        public override bool Equals( object obj )
        {
            return obj is Cell other && Equals( other );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( Row, Column, Links, Neighbors );
        }

        public static bool operator ==( in ProtoCell lhs, in ProtoCell rhs )
        {
            return lhs?.Equals( rhs ) ?? false;
        }

        public static bool operator !=( in ProtoCell cell1, in ProtoCell cell2 )
        {
            return !( cell1 == cell2 );
        }

        private bool CanLink( in ProtoCell other )
        {
            return Row != other.Row || Column != other.Column;
        }
    }
}