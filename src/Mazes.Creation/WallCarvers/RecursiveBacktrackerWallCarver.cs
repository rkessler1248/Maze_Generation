using System.Collections.Generic;
using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public class RecursiveBacktrackerWallCarver : WallCarver
    {
        public override void Carve( Maze maze )
        {
            Stack<Cell> path = new Stack<Cell>();
            path.Push( Random( maze ) );
            while ( path.Count > 0 )
            {
                Cell next = RandomUnvisitedNeighbor( path.Peek() );
                if ( next is null )
                {
                    Backtrack( path );
                }
                else
                {
                    path.Peek().LinkTo( next );
                    path.Push( next );
                }
            }
        }

        public void Backtrack( Stack<Cell> path )
        {
            if ( path.Count == 0 )
            {
                return;
            }

            Cell candidate = RandomUnvisitedNeighbor( path.Peek() );
            while ( candidate is null && path.Count > 0 )
            {
                path.Pop();
                if ( path.Count > 0 )
                {
                    candidate = RandomUnvisitedNeighbor( path.Peek() );
                }
            }

            if ( candidate is null )
            {
                return;
            }

            path.Peek().LinkTo( candidate );
            path.Push( candidate );
        }

        private Cell RandomUnvisitedNeighbor( Cell cell )
        {
            ISet<Cell> unvisitedNeighbors = new HashSet<Cell>();
            foreach ( Cell neighbor in cell.Neighbors )
            {
                if ( !Visited( neighbor ) )
                {
                    unvisitedNeighbors.Add( neighbor );
                }
            }

            return Random( unvisitedNeighbors, unvisitedNeighbors.Count );
        }
    }
}