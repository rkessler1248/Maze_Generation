using System;
using System.Security.Cryptography;

namespace MazeGeneration.Utility
{
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        public static int NextInt( int minimum, int maximum )
        {
            var bytes = new byte[ 256 ];
            Provider.GetBytes( bytes );
            var random = Math.Abs( BitConverter.ToInt32( bytes, 0 ) );
            return random % ( maximum - minimum + 1 ) + minimum;
        }
    }
}