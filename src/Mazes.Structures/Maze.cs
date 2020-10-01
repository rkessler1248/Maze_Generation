using System.Collections.Generic;

namespace Mazes.Structures
{
    public class Maze
    {
        protected readonly Cell[ , ] Cells;

        public readonly int NumberOfColumns;
        public readonly int NumberOfRows;
        public readonly long Size;

        public Maze( int rows, int columns )
        {
            NumberOfRows = rows;
            NumberOfColumns = columns;
            Size = NumberOfRows * NumberOfColumns;

            Cells = new Cell[ NumberOfRows, NumberOfColumns ];

            InitializeMazeCells( Cells );
            InitializeMazeCellNeighbors( Cells );
        }

        protected Maze( int rows, int columns, long size )
        {
            NumberOfRows = rows;
            NumberOfColumns = columns;
            Size = size;

            Cells = new Cell[ NumberOfRows, NumberOfColumns ];
        }

        public Cell this[ in int row, in int column ] => Cells[ row, column ];

        public IReadOnlyCollection<Cell> DeadEnds()
        {
            HashSet<Cell> result = new HashSet<Cell>();

            for ( int row = 0; row < NumberOfRows; ++row )
            for ( int column = 0; column < NumberOfColumns; ++column )
            {
                if ( Cells[ row, column ].Links.Count == 1 )
                {
                    result.Add( Cells[ row, column ] );
                }
            }

            return result;
        }

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