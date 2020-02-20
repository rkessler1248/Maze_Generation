namespace MazeGeneration.Abstractions.Algorithms.Solvers
{
    public interface IDijkstraEnabledCell<TCell, TCoordinates> where TCell : ICell<TCoordinates>
                                                               where TCoordinates : ICoordinates
    {
        IDistances<TCell, TCoordinates> Distances();
    }
}