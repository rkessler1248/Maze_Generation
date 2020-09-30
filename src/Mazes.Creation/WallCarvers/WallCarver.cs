using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public abstract class WallCarver : MazeCreator
    {
        public abstract void Carve( Maze maze );
    }
}