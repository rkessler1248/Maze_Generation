using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazeGeneration.Utility
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>( this ICollection<T> collection )
        {
            return collection.Count == 0;
        }

        public static T GetRandom<T>( this ICollection<T> collection )
        {
            return collection.ElementAt( RandomNumberGenerator.NextInt( 0, collection.Count ) );
        }

        public static T GetRandom<T>( this ICollection collection )
        {
            return collection.Cast<T>().ElementAt( RandomNumberGenerator.NextInt( 0, collection.Count ) );
        }
    }
}