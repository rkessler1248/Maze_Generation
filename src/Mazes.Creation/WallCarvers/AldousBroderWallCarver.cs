using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public class AldousBroderWallCarver : WallCarver
    {
        public override void Carve( Maze maze )
        {
            long unvisited = maze.Size - 1;
            Cell current = Random( maze );
            while ( unvisited > 0 )
            {
                Cell candidate = Random( current.Neighbors, current.Neighbors.Count );
                if ( ShouldLinkToCurrent( candidate ) )
                {
                    candidate.LinkTo( current );
                    --unvisited;
                }

                current = candidate;
            }
        }

        public bool ShouldLinkToCurrent( Cell candidate ) => !Visited( candidate );
    }
}