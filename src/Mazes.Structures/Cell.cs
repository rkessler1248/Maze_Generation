using System.Collections.Generic;

namespace Mazes.Structures
{
    public class Cell
    {
        private readonly Coordinate _coordinates;

        private readonly HashSet<Cell> _links;
        private readonly HashSet<Cell> _neighbors;

        public Cell( in Coordinate coordinates ) => ( _coordinates, _links, _neighbors ) = ( coordinates, new HashSet<Cell>(), new HashSet<Cell>() );

        public Cell( in int column, in int row )
            : this( new Coordinate( column, row ) ) { }

        public int Column => _coordinates[ 0 ];
        public int Row => _coordinates[ 1 ];

        public IReadOnlyCollection<Cell> Links => _links;
        public IReadOnlyCollection<Cell> Neighbors => _neighbors;

        public void SetNeighbor( Cell other )
        {
            if ( this == other || _coordinates == other._coordinates || IsNeighbor( other ) )
            {
                return;
            }
            
            _neighbors.Add( other );
            other.SetNeighbor( this );
        }

        public bool IsNeighbor( Cell other ) => _neighbors.Contains( other );

        public void LinkTo( Cell other )
        {
            if ( this == other || _coordinates == other._coordinates || IsLinkedTo( other ) )
            {
                return;
            }

            _links.Add( other );
            other.LinkTo( this );
        }

        public bool IsLinkedTo( Cell other ) => _links.Contains( other );
    }
}