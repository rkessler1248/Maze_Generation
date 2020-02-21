using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Creators;
using RNG = MazeGeneration.Utility.RandomNumberGenerator;

namespace MazeGeneration.Models
{
    public abstract class Maze<TCell, TCoordinates, TCollection> : IBinaryTreeEnabledMaze<TCell, TCoordinates, TCollection>,
                                                                   ISidewinderEnabledMaze<TCell, TCoordinates, TCollection>
        where TCell : class, ICell<TCoordinates>
        where TCoordinates : ICoordinates
        where TCollection : ICollection
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
                for ( int dimension = 0; dimension < Dimensions; ++dimension )
                {
                    product *= DimensionSizes[ dimension ];
                }

                return product;
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

        public void Reset( bool linkAllCells = false )
        {
            PrepareMaze();
            ConfigureCells( linkAllCells );
        }

        public abstract void ForEachCell( Action<TCoordinates> action );

        public TCell GetRandomCell()
        {
            var key = new int[ Dimensions ];
            for ( int i = 0; i < Dimensions; ++i )
            {
                key[ i ] = RNG.NextInt( 0, DimensionSizes[ i ] );
            }

            return this[ key ];
        }

        public abstract string Print();

        public abstract IList<TCell> NeighborsToChooseFrom( TCoordinates coordinates );

        public abstract bool CloseOutRun( TCoordinates coordinates, bool randomResult );
        public abstract void LinkCellToCloseOutNeighbor( TCell cell );
        public abstract void LinkCellToContinueNeighbor( TCell cell );

        protected abstract void PrepareMaze();
        protected abstract void ConfigureCells( bool linkAllCells );

        protected abstract bool KeysAreValid( int[] keys );
        protected abstract TCell GetCell( int[] keys );
        protected abstract void SetCell( int[] keys, TCell value );

        private static int[] BreakId( string cellId )
        {
            var idParts = cellId.Split( new[] { Cell<TCoordinates>.IdSeparator }, StringSplitOptions.None );
            return idParts.Select( int.Parse ).ToArray();
        }
    }
}