namespace MazeGeneration.Abstractions.Orthogonal
{
    public interface IOrthogonalCoordinates2D : ICoordinates
    {
        int Row { get; set; }
        int Column { get; set; }
    }
}