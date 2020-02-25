using System.Collections.Generic;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Orthogonal;
using MazeGeneration.Utility.Extensions;

namespace MazeGeneration.Models.Orthogonal
{
    public class OrthogonalCell2D : Cell<IOrthogonalCoordinates2D>, IOrthogonalCell2D
    {
        private string _easternNeighbor;
        private string _northernNeighbor;
        private string _southernNeighbor;
        private string _westernNeighbor;

        public OrthogonalCell2D( int row, int column )
        {
            Coordinates = new OrthogonalCoordinates2D { Row = row, Column = column };
            Tessellation = Tessellation.Gamma;
            Neighbors = new Dictionary<string, ICell<IOrthogonalCoordinates2D>>();
            Links = new Dictionary<string, ICell<IOrthogonalCoordinates2D>>();
        }

        public override void AddNeighbor( ICell<IOrthogonalCoordinates2D> cell, bool addBidirectionally = true )
        {
            base.AddNeighbor( cell, addBidirectionally );

            if ( Neighbors.ContainsKey( cell.Id ) )
            {
                SetNeighbor( cell as IOrthogonalCell2D );
            }
        }

        public override void RemoveNeighbor( ICell<IOrthogonalCoordinates2D> cell, bool removeBidirectionally = true )
        {
            base.RemoveNeighbor( cell, removeBidirectionally );
            if ( !Neighbors.ContainsKey( cell.Id ) )
            {
                UnsetNeighbor( cell as IOrthogonalCell2D );
            }
        }

        public IOrthogonalCell2D NorthernNeighbor => !string.IsNullOrEmpty( _northernNeighbor ) && Neighbors.ContainsKey( _northernNeighbor )
                                                         ? ( IOrthogonalCell2D ) Neighbors[ _northernNeighbor ]
                                                         : null;

        public IOrthogonalCell2D EasternNeighbor => !string.IsNullOrEmpty( _easternNeighbor ) && Neighbors.ContainsKey( _easternNeighbor )
                                                        ? ( IOrthogonalCell2D ) Neighbors[ _easternNeighbor ]
                                                        : null;

        public IOrthogonalCell2D SouthernNeighbor => !string.IsNullOrEmpty( _southernNeighbor ) && Neighbors.ContainsKey( _southernNeighbor )
                                                         ? ( IOrthogonalCell2D ) Neighbors[ _southernNeighbor ]
                                                         : null;

        public IOrthogonalCell2D WesternNeighbor => !string.IsNullOrEmpty( _westernNeighbor ) && Neighbors.ContainsKey( _westernNeighbor )
                                                        ? ( IOrthogonalCell2D ) Neighbors[ _westernNeighbor ]
                                                        : null;

        public void SetNeighbor( IOrthogonalCell2D cell )
        {
            if ( cell.IsNull() || cell == this )
            {
                return;
            }

            if ( IsNorthernNeighbor( cell ) )
            {
                _northernNeighbor = cell.Id;
            }
            else if ( IsEasternNeighbor( cell ) )
            {
                _easternNeighbor = cell.Id;
            }
            else if ( IsSouthernNeighbor( cell ) )
            {
                _southernNeighbor = cell.Id;
            }
            else if ( IsWesternNeighbor( cell ) )
            {
                _westernNeighbor = cell.Id;
            }
        }

        public void UnsetNeighbor( IOrthogonalCell2D cell )
        {
            if ( cell.IsNull() || cell == this )
            {
                return;
            }

            if ( IsNorthernNeighbor( cell ) )
            {
                _northernNeighbor = null;
            }
            else if ( IsEasternNeighbor( cell ) )
            {
                _easternNeighbor = null;
            }
            else if ( IsSouthernNeighbor( cell ) )
            {
                _southernNeighbor = null;
            }
            else if ( IsWesternNeighbor( cell ) )
            {
                _westernNeighbor = null;
            }
        }

        private bool IsNorthernNeighbor( IOrthogonalCell2D other )
        {
            return other.Coordinates.Column == Coordinates.Column && other.Coordinates.Row == Coordinates.Row - 1;
        }

        private bool IsEasternNeighbor( IOrthogonalCell2D other )
        {
            return other.Coordinates.Column == Coordinates.Column + 1 && other.Coordinates.Row == Coordinates.Row;
        }

        private bool IsSouthernNeighbor( IOrthogonalCell2D other )
        {
            return other.Coordinates.Column == Coordinates.Column && other.Coordinates.Row == Coordinates.Row + 1;
        }

        private bool IsWesternNeighbor( IOrthogonalCell2D other )
        {
            return other.Coordinates.Column == Coordinates.Column - 1 && other.Coordinates.Row == Coordinates.Row;
        }
    }
}