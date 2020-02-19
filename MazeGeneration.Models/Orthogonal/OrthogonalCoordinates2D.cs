using MazeGeneration.Abstractions.Orthogonal;

namespace MazeGeneration.Models.Orthogonal
{
    public class OrthogonalCoordinates2D : Coordinates, IOrthogonalCoordinates2D
    {
        public OrthogonalCoordinates2D() : base( 2 ) { }

        public int Row
        {
            get => Values[ 0 ];
            set => Values[ 0 ] = value;
        }

        public int Column
        {
            get => Values[ 1 ];
            set => Values[ 1 ] = value;
        }
    }
}