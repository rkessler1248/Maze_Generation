using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Creators;
using MazeGeneration.Utility;

namespace MazeGeneration.Models
{
    public abstract class Maze<TCell, TCoordinates, TCollection> : IBinaryTreeEnabledMaze<TCell, TCoordinates, TCollection>,
                                                                   ISidewinderEnabledMaze<TCell, TCoordinates, TCollection>
        where TCell : class, ICell<TCoordinates>
        where TCoordinates : ICoordinates
        where TCollection : class, ICollection
    {
        protected Maze( int dimensions, IList<int> dimensionSizes )
        {
            Dimensions = dimensions;
            DimensionSizes = dimensionSizes;
        }

        public int Dimensions { get; }
        public IList<int> DimensionSizes { get; }

        public int Size
        {
            get
            {
                var product = 1;
                for ( var dimension = 0; dimension < Dimensions; ++dimension )
                {
                    product *= DimensionSizes[ dimension ];
                }

                return product - Cells.Cast<object>().Count( o => o.IsNull() );
            }
        }

        public abstract TCollection Cells { get; }

        public TCell this[ params int[] keys ]
        {
            get => KeysAreValid( keys ) ? GetCell( keys ) : null;
            set
            {
                if ( KeysAreValid( keys ) )
                {
                    SetCell( keys, value );
                }
            }
        }

        public TCell this[ TCoordinates coordinates ]
        {
            get => this[ coordinates.Values.ToArray() ];
            set => this[ coordinates.Values.ToArray() ] = value;
        }

        public TCell this[ string id ]
        {
            get => this[ BreakId( id ) ];
            set => this[ BreakId( id ) ] = value;
        }

        public void Reset( bool linkAllCells = false, IList<TCoordinates> mask = null )
        {
            PrepareMaze();
            ConfigureCells( linkAllCells );
            if ( mask.IsNotNull() )
            {
                ApplyMask( mask );
            }
        }

        public abstract void ForEachCell( Action<TCoordinates> action );
        public abstract void ForEachCell( Action<TCell> action );

        public TCell GetRandomCell()
        {
            return Cells.GetRandom<TCell>();
        }

        public IList<TCell> DeadEnds()
        {
            return Cells.Cast<TCell>()
                        .Where( cell => cell.IsNotNull() && cell.Links.Count == 1 )
                        .ToList();
        }

        public abstract string Print();

        public abstract IList<TCell> NeighborsToChooseFrom( TCoordinates coordinates );

        public abstract bool CloseOutRun( TCoordinates coordinates, bool randomResult );
        public abstract void LinkCellToCloseOutNeighbor( TCell cell );
        public abstract void LinkCellToContinueNeighbor( TCell cell );

        protected abstract void PrepareMaze();
        protected abstract void ConfigureCells( bool linkAllCells = false );

        protected abstract bool KeysAreValid( int[] keys );
        protected abstract TCell GetCell( int[] keys );
        protected abstract void SetCell( int[] keys, TCell value );

        private static int[] BreakId( string cellId )
        {
            var idParts = cellId.Split( new[] { Cell<TCoordinates>.IdSeparator }, StringSplitOptions.None );
            return idParts.Select( int.Parse ).ToArray();
        }

        private void ApplyMask( IList<TCoordinates> mask )
        {
            var removedCellIds = new List<string>();
            foreach ( var coordinates in mask )
            {
                removedCellIds.Add( this[ coordinates ].Id );
                this[ coordinates ] = null;
            }

            foreach ( TCell cell in Cells )
            {
                if ( cell is null )
                {
                    continue;
                }

                foreach ( var removedCellId in removedCellIds )
                {
                    if ( cell.Neighbors.ContainsKey( removedCellId ) )
                    {
                        cell.Neighbors.Remove( removedCellId );
                    }
                }
            }
        }
    }
}