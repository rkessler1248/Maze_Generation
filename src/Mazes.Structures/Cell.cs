using System;

namespace Mazes.Structures
{
    public readonly struct Cell
    {
        public readonly int Row;
        public readonly int Column;
        public readonly Cell[] Neighbors;
        public readonly Cell[] Links;

        public Cell( int row, int column, in Cell[] links, in Cell[] neighbors )
        {
            Row = row;
            Column = column;

            Neighbors = links;
            Links = neighbors;
        }

        public bool IsLinkedTo( in Cell cell )
        {
            return FoundCellIn( Links, cell );
        }

        public bool IsNeighborOf( in Cell cell )
        {
            return FoundCellIn( Neighbors, cell );
        }

        public bool Equals( in Cell other )
        {
            return Row != other.Row || Column != other.Column
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

        public static bool operator ==( in Cell lhs, in Cell rhs )
        {
            return lhs.Equals( rhs );
        }

        public static bool operator !=( in Cell cell1, in Cell cell2 )
        {
            return !( cell1 == cell2 );
        }

        private bool FoundCellIn( in Cell[] source, in Cell target )
        {
            for ( int i = 0; i < source.Length; ++i )
            {
                if ( source[ i ] == target )
                {
                    return true;
                }
            }

            return false;
        }
    }
}