namespace Mazes.Structures
{
    public class Maze
    {
        private readonly Cell[ , ] _cells;

        public readonly int NumberOfColumns;
        public readonly int NumberOfRows;

        public Maze( int rows, int columns )
        {
            NumberOfColumns = columns;
            NumberOfRows = rows;

            _cells = new Cell[ NumberOfRows, NumberOfColumns ];
            
            InitializeMazeCells( _cells );
            InitializeMazeCellNeighbors( _cells );
        }

        public Cell this[ in int row, in int column ] => _cells[ row, column ];

        private void InitializeMazeCells( Cell[ , ] cells )
        {
            for ( int row = 0; row < NumberOfRows; ++row )
            for ( int column = 0; column < NumberOfColumns; ++column )
            {
                cells[ row, column ] = new Cell( column, row );
            }
        }

        private void InitializeMazeCellNeighbors( Cell[ , ] cells )
        {
            for ( int row = 0; row < NumberOfRows; ++row )
            for ( int column = 0; column < NumberOfColumns; ++column )
            {
                Cell current = cells[ row, column ];

                if ( row > 0 )
                {
                    Cell northern = cells[ row - 1, column ];
                    current.SetNeighbor( northern );
                }

                if ( row < NumberOfRows - 1 )
                {
                    Cell southern = cells[ row + 1, column ];
                    current.SetNeighbor( southern );
                }

                if ( column > 0 )
                {
                    Cell western = cells[ row, column - 1 ];
                    current.SetNeighbor( western );
                }

                if ( column < NumberOfColumns - 1 )
                {
                    Cell eastern = cells[ row, column + 1 ];
                    current.SetNeighbor( eastern );
                }
            }
        }
    }
}