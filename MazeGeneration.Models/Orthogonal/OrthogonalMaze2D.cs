using System;
using System.Collections.Generic;
using System.Text;
using MazeGeneration.Abstractions.Orthogonal;
using MazeGeneration.Utility;

namespace MazeGeneration.Models.Orthogonal
{
    public class OrthogonalMaze2D : Maze<IOrthogonalCell2D, IOrthogonalCoordinates2D, IOrthogonalCell2D[ , ]>,
                                    IOrthogonalMaze2D
    {
        public OrthogonalMaze2D( int numberOfRows, int numberOfColumns ) : base( 2, new List<int> { numberOfRows, numberOfColumns } )
        {
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
            Cells = new IOrthogonalCell2D[ numberOfRows, numberOfColumns ];
        }

        public override IOrthogonalCell2D[ , ] Cells { get; }

        public override void ForEachCell( Action<IOrthogonalCoordinates2D> action )
        {
            for ( var row = 0; row < NumberOfRows; ++row )
            for ( var column = 0; column < NumberOfColumns; ++column )
            {
                action( this[ row, column ].Coordinates );
            }
        }

        public override void ForEachCell( Action<IOrthogonalCell2D> action )
        {
            for ( var row = 0; row < NumberOfRows; ++row )
            for ( var column = 0; column < NumberOfColumns; ++column )
            {
                action( this[ row, column ] );
            }
        }

        public override string Print()
        {
            var output = new StringBuilder();
            output.Append( "+" );
            for ( var column = 0; column < NumberOfColumns; ++column )
            {
                output.Append( "---+" );
            }

            output.AppendLine();

            for ( var row = 0; row < NumberOfRows; ++row )
            {
                string top = "|", bottom = "+";

                for ( var column = 0; column < NumberOfColumns; ++column )
                {
                    var cell = Cells[ row, column ];
                    var eastBoundary = cell.IsLinkedTo( cell.EasternNeighbor ) ? " " : "|";
                    top += $"   {eastBoundary}";

                    var southBoundary = cell.IsLinkedTo( cell.SouthernNeighbor ) ? "   " : "---";
                    bottom += $"{southBoundary}+";
                }

                output.AppendLine( top );
                output.AppendLine( bottom );
            }

            return output.ToString();
        }

        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }

        protected override void PrepareMaze()
        {
            for ( var row = 0; row < NumberOfRows; ++row )
            for ( var column = 0; column < NumberOfColumns; ++column )
            {
                Cells[ row, column ] = new OrthogonalCell2D( row, column );
            }
        }

        protected override void ConfigureCells( bool linkAllCells )
        {
            for ( var row = 0; row < NumberOfRows; ++row )
            for ( var column = 0; column < NumberOfColumns; ++column )
            {
                if ( row > 0 ) // Northern Neighbor
                {
                    Cells[ row, column ].AddNeighbor( Cells[ row - 1, column ] );
                    if ( linkAllCells )
                    {
                        Cells[ row, column ].LinkTo( Cells[ row - 1, column ] );
                    }
                }

                if ( row < NumberOfRows - 1 ) // Southern Neighbor
                {
                    Cells[ row, column ].AddNeighbor( Cells[ row + 1, column ] );
                    if ( linkAllCells )
                    {
                        Cells[ row, column ].LinkTo( Cells[ row + 1, column ] );
                    }
                }

                if ( column > 0 ) // Western Neighbor
                {
                    Cells[ row, column ].AddNeighbor( Cells[ row, column - 1 ] );
                    if ( linkAllCells )
                    {
                        Cells[ row, column ].LinkTo( Cells[ row, column - 1 ] );
                    }
                }

                if ( column < NumberOfColumns - 1 ) // Eastern Neighbor
                {
                    Cells[ row, column ].AddNeighbor( Cells[ row, column + 1 ] );
                    if ( linkAllCells )
                    {
                        Cells[ row, column ].LinkTo( Cells[ row, column + 1 ] );
                    }
                }
            }
        }

        public override IList<IOrthogonalCell2D> NeighborsToChooseFrom( IOrthogonalCoordinates2D coordinates )
        {
            var cell = this[ coordinates.Row, coordinates.Column ];
            return new List<IOrthogonalCell2D> { cell.NorthernNeighbor, cell.EasternNeighbor };
        }

        public override bool CloseOutRun( IOrthogonalCoordinates2D coordinates, bool randomResult )
        {
            var cell = this[ coordinates ];
            
            var atEasternBoundary = cell.EasternNeighbor.IsNull();
            var atNorthernBoundary = cell.NorthernNeighbor.IsNull();
            
            return atEasternBoundary || !atNorthernBoundary && randomResult;
        }

        public override void LinkCellToContinueNeighbor( IOrthogonalCell2D cell )
        {
            cell.LinkTo( cell.EasternNeighbor );
        }

        public override void LinkCellToCloseOutNeighbor( IOrthogonalCell2D cell )
        {
            if ( cell.NorthernNeighbor.IsNull() )
            {
                return;
            }

            cell.LinkTo( cell.NorthernNeighbor );
        }

        protected override bool KeysAreValid( int[] keys )
        {
            return keys.Length > 0
                   && keys.Length < 3
                   && keys[ 0 ] >= 0
                   && keys[ 0 ] < NumberOfRows
                   && keys[ 1 ] >= 0
                   && keys[ 1 ] < NumberOfColumns;
        }

        protected override IOrthogonalCell2D GetCell( int[] keys )
        {
            return Cells[ keys[ 0 ], keys[ 1 ] ];
        }

        protected override void SetCell( int[] keys, IOrthogonalCell2D value )
        {
            Cells[ keys[ 0 ], keys[ 1 ] ] = value;
        }
    }
}