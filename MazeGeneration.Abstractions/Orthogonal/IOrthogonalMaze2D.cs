namespace MazeGeneration.Abstractions.Orthogonal
{
    public interface IOrthogonalMaze2D : IMaze<IOrthogonalCell2D, IOrthogonalCoordinates2D, IOrthogonalCell2D[ , ]>
    {
        int NumberOfRows { get; set; }
        int NumberOfColumns { get; set; }
    }
}