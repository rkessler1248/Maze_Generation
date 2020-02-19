namespace MazeGeneration.Abstractions.Orthogonal
{
    public interface IOrthogonalCell2D : ICell<IOrthogonalCoordinates2D>
    {
        IOrthogonalCell2D NorthernNeighbor { get; }
        IOrthogonalCell2D EasternNeighbor { get; }
        IOrthogonalCell2D SouthernNeighbor { get; }
        IOrthogonalCell2D WesternNeighbor { get; }

        void SetNeighbor( IOrthogonalCell2D cell );
        void UnsetNeighbor( IOrthogonalCell2D cell );
    }
}