namespace Mazes.Structures
{
    public class Maze
    {
        private readonly Cell[ , ] _cells;

        public Maze( int columns, int rows )
        {
            Columns = columns;
            Rows = rows;

            _cells = new Cell[ Rows, Columns ];

            InitializeCells();
            AssignNeighbors();
        }

        public int Columns { get; }
        public int Rows { get; }

        public Cell this[ int row, int column ] => _cells[ row, column ];

        private void InitializeCells()
        {
            for ( int row = 0; row < Rows; ++row )
            for ( int column = 0; column < Columns; ++column )
            {
                _cells[ row, column ] = new Cell( row, column );
            }
        }

        private void AssignNeighbors()
        {
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
    }
}