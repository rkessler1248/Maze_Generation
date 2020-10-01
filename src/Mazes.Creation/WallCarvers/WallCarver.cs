using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public abstract class WallCarver : MazeCreator
    {
        public abstract void Carve( Maze maze );
        protected override bool Visited( Cell cell ) => cell.Links.Count > 0;
    }
}