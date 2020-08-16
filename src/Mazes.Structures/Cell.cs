using System;
using System.Collections.Generic;

namespace Mazes.Structures
{
    public class Cell
    {
        public string Id => $"{Row.ToString()}-{Column.ToString()}";
        public int Row { get; }
        public int Column { get; }

        private readonly IDictionary<string, Cell> _neighbors = new Dictionary<string, Cell>();
        private readonly IDictionary<string, Cell> _links = new Dictionary<string, Cell>();

        public Cell( int row, int column )
        {
            Row = row;
            Column = column;
        }

        public void LinkTo( Cell other )
        {
            if ( this != other && !IsLinkedTo( other ) )
            {
                _links[ other.Id ] = other;
                other.LinkTo( this );
            }
        }

        public void MakeNeighbors( Cell other )
        {
            if ( this != other && !IsNeighborOf( other ) )
            {
                _neighbors[ other.Id ] = other;
                other.MakeNeighbors( this );
            }
        }

        public bool IsLinkedTo( Cell other )
        {
            return _links.ContainsKey( other.Id ) && _links[ other.Id ].Equals( other );
        }

        public bool IsNeighborOf( Cell other )
        {
            return _neighbors.ContainsKey( other.Id ) && _neighbors[ other.Id ].Equals( other );
        }

        public bool Equals( Cell other )
        {
            return Row == other.Row
                   && Column == other.Column
                   && Equals( _links, other._links )
                   && Equals( _links, other._links );
        }

        public override bool Equals( object obj )
        {
            return obj is Cell other && Equals( other );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( Row, Column, _links, _neighbors );
        }

        public static bool operator ==( Cell lhs, Cell rhs )
        {
            return lhs?.Equals( rhs ) ?? false;
        }

        public static bool operator !=( Cell lhs, Cell rhs )
        {
            return !( lhs == rhs );
        }
    }
}