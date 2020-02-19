namespace MazeGeneration.Abstractions
{
    /// <summary>The shape of a maze's cells.</summary>
    public enum Tessellation
    {
        /// <summary>Standard/Orthogonal</summary>
        Gamma = 0,

        /// <summary>Triangular</summary>
        Delta = 1,

        /// <summary>Hexagonal</summary>
        Sigma = 2,

        /// <summary>Concentric, circular passages</summary>
        Theta = 3,

        /// <summary>Interlocking Octagons and Squares</summary>
        Upsilon = 4,

        /// <summary>Rectangular, but passages between cells can also be at 45 degree angles in addition to horizontal and vertical</summary>
        Zeta = 5,

        /// <summary>Any non-orthogonal tessellation</summary>
        Omega = 6,

        /// <summary>No consistent tessellation; walls and passages are random angles</summary>
        Crack = 7,

        /// <summary>Composed of smaller mazes</summary>
        Fractal = 8
    }
}