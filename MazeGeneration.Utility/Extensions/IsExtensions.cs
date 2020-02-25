namespace MazeGeneration.Utility.Extensions
{
    public static class IsExtensions
    {
        public static bool IsNull( this object obj )
        {
            return obj is null;
        }

        public static bool IsNotNull( this object obj )
        {
            return !obj.IsNull();
        }
    }
}