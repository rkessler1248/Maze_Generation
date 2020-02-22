using System.Collections.Generic;

namespace MazeGeneration.Utility
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>( this ICollection<T> collection )
        {
            return collection.Count == 0;
        }
    }
}