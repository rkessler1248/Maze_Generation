namespace Mazes.Structures
{
    public class ProtoMaze
    {
        private readonly ProtoCell[ , ] _cells;

        public ProtoMaze( int columns, int rows )
        {
            Columns = columns;
            Rows = rows;

            _cells = new ProtoCell[ Rows, Columns ];

            for ( int row = 0; row < Rows; ++row )
            for ( int column = 0; column < Columns; ++column )
            {
                _cells[ row, column ] = new ProtoCell( row, column );
            }

            for ( int row = 0; row < Rows; ++row )
            for ( int column = 0; column < Columns; ++column )
            {
                if ( row > 0 )
                {
                    _cells[ row, column ].MakeNeighbors( _cells[ row - 1, column ] );
                }

                if ( row < Rows - 1 )
                {
                    _cells[ row, column ].MakeNeighbors( _cells[ row + 1, column ] );
                }

                if ( column > 0 )
                {
                    _cells[ row, column ].MakeNeighbors( _cells[ row, column - 1 ] );
                }

                if ( column < Columns - 1 )
                {
                    _cells[ row, column ].MakeNeighbors( _cells[ row, column + 1 ] );
                }
            }
        }

        public int Columns { get; }
        public int Rows { get; }

        public ProtoCell this[ int row, int column ] => _cells[ row, column ];

        public bool IsCornerCell( ProtoCell cell )
        {
            return cell.Row == 0 && cell.Column == 0
                   || cell.Row == 0 && cell.Column == Columns - 1
                   || cell.Row == Rows - 1 && cell.Column == 0
                   || cell.Row == Rows - 1 && cell.Column == Columns - 1;
        }

        public bool IsEdgeCell( ProtoCell cell )
        {
            return cell.Row == 0 || cell.Row == Rows - 1 || cell.Column == 0 || cell.Column == Columns - 1;
        }
    }
}