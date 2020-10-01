using System;

namespace Mazes.Structures
{
    public class MaskedMaze : Maze
    {
        public MaskedMaze( int rows, int columns, bool[ , ] mask ) : base( rows, columns, MaskSize( mask ) )
        {
            if ( mask.GetLength( 0 ) != Cells.GetLength( 0 ) && mask.GetLength( 1 ) != Cells.GetLength( 1 ) )
            {
                throw new ArgumentException( nameof(mask) );
            }

            InitializeCellsWithMask( mask );
            InitializeMazeCellNeighborsWithMask( mask );
        }

        private void InitializeCellsWithMask( bool[ , ] mask )
        {
            for ( int row = 0; row < NumberOfRows; ++row )
            for ( int column = 0; column < NumberOfColumns; ++column )
            {
                if ( mask[ row, column ] )
                {
                    Cells[ row, column ] = new Cell( column, row );
                }
            }
        }

        private void InitializeMazeCellNeighborsWithMask( bool[ , ] mask )
        {
            for ( int row = 0; row < NumberOfRows; ++row )
            for ( int column = 0; column < NumberOfColumns; ++column )
            {
                if ( !mask[ row, column ] )
                {
                    continue;
                }

                Cell current = Cells[ row, column ];

                if ( row > 0 && mask[ row - 1, column ] )
                {
                    Cell northern = Cells[ row - 1, column ];
                    current.SetNeighbor( northern );
                }

                if ( row < NumberOfRows - 1 && mask[ row + 1, column ] )
                {
                    Cell southern = Cells[ row + 1, column ];
                    current.SetNeighbor( southern );
                }

                if ( column > 0 && mask[ row, column - 1 ] )
                {
                    Cell western = Cells[ row, column - 1 ];
                    current.SetNeighbor( western );
                }

                if ( column < NumberOfColumns - 1 && mask[ row, column + 1 ] )
                {
                    Cell eastern = Cells[ row, column + 1 ];
                    current.SetNeighbor( eastern );
                }
            }
        }

        private static long MaskSize( bool[ , ] mask )
        {
            long size = 0;
            for ( int row = 0; row < mask.GetLength( 0 ); ++row )
            for ( int column = 0; column < mask.GetLength( 1 ); ++column )
            {
                if ( mask[ row, column ] )
                {
                    ++size;
                }
            }

            return size;
        }
    }
}