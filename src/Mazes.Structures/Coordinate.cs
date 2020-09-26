namespace Mazes.Structures
{
    public readonly struct Coordinate
    {
        public int Dimensions => _values.Length;

        private readonly int[] _values;
        public Coordinate( params int[] values ) => _values = values ?? new int[ 0 ];

        public int this[ int dimension ] => _values[ dimension ];

        public override bool Equals( object obj )
        {
            if ( obj is Coordinate other )
            {
                return Equals( in other );
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = _values.Length.GetHashCode();
            for ( int i = 0; i < _values.Length; ++i )
            {
                result |= _values[ i ].GetHashCode();
            }

            return result;
        }

        public static bool operator ==( in Coordinate lhs, in Coordinate rhs )
        {
            return lhs.Equals( in rhs );
        }

        public static bool operator !=( in Coordinate lhs, in Coordinate rhs )
        {
            return !( lhs == rhs );
        }

        private bool Equals( in Coordinate other )
        {
            if ( other.Dimensions != Dimensions )
            {
                return false;
            }

            for ( int i = 0; i < Dimensions; ++i )
            {
                if ( _values[ i ] != other._values[ i ] )
                {
                    return false;
                }
            }

            return true;
        }
    }
}