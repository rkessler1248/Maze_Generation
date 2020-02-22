using System;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Utility;

namespace MazeGeneration.Models
{
    public abstract class Coordinates : ICoordinates
    {
        protected Coordinates( int dimensions )
        {
            Dimensions = dimensions;
            Values = Enumerable.Repeat( 0, Dimensions ).ToList();
        }

        public int Dimensions { get; }
        public IList<int> Values { get; }

        public int this[ int dimension ]
        {
            get
            {
                ValidateDimension( dimension );
                return Values[ dimension ];
            }
            set
            {
                ValidateDimension( dimension );
                Values[ dimension ] = value;
            }
        }

        public override bool Equals( object obj )
        {
            if ( obj is Coordinates other )
            {
                return Equals( other );
            }

            return false;
        }

        protected bool Equals( Coordinates other )
        {
            for ( var dimension = 0; dimension < Dimensions; ++dimension )
            {
                if ( this[ dimension ] != other[ dimension ] )
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==( Coordinates lhs, Coordinates rhs )
        {
            return Equals( lhs, rhs );
        }

        public static bool operator !=( Coordinates lhs, Coordinates rhs )
        {
            return !( lhs == rhs );
        }

        public override int GetHashCode()
        {
            var hash = 0;
            if ( Values.IsNull() )
            {
                return hash;
            }

            for ( var dimension = 0; dimension < Dimensions; ++dimension )
            {
                hash ^= this[ dimension ].GetHashCode();
            }

            return hash;
        }

        private void ValidateDimension( int dimension )
        {
            if ( dimension >= 0 && dimension < Dimensions )
            {
                return;
            }

            throw new IndexOutOfRangeException( $"Dimension is zero-based and must be less than {Dimensions}" );
        }
    }
}