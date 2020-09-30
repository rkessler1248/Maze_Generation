using System.Collections.Generic;
using System.Linq;
using Mazes.Structures;

namespace Mazes.Creation.WallCarvers
{
    public class WilsonsWallCarver : WallCarver
    {
        public override void Carve( Maze maze )
        {
            ISet<Cell> unvisited = InitializeUnvisited( maze );
            ISet<Cell> visited = new HashSet<Cell> { Random( unvisited, unvisited.Count ) };

            unvisited.Remove( visited.First() );

            Stack<Cell> path = new Stack<Cell>();

            while ( unvisited.Count > 0 )
            {
                if ( path.Count == 0 )
                {
                    path.Push( Random( unvisited, unvisited.Count ) );
                }
                else
                {
                    path.Push( Random( path.Peek().Neighbors, path.Peek().Neighbors.Count ) );
                    EraseLoop( path );
                }

                if ( IntersectedVisited( visited, path ) )
                {
                    CarvePath( visited, unvisited, path );
                }
            }
        }

        public void EraseLoop( Stack<Cell> path )
        {
            if ( path.Distinct().Count() == path.Count )
            {
                return;
            }

            Cell head = path.Pop();
            while ( path.Peek() != head )
            {
                path.Pop();
            }
        }

        public bool IntersectedVisited( ISet<Cell> visited, Stack<Cell> path )
        {
            return path.TryPeek( out Cell top ) && visited.Contains( top );
        }

        public void CarvePath( ISet<Cell> visited, ISet<Cell> unvisited, Stack<Cell> path )
        {
            Cell current = path.Pop();
            visited.Add( current );
            unvisited.Remove( current );

            while ( path.Count > 0 )
            {
                current.LinkTo( path.Peek() );
                current = path.Pop();
                visited.Add( current );
                unvisited.Remove( current );
            }
        }

        private static ISet<Cell> InitializeUnvisited( Maze maze )
        {
            ISet<Cell> result = new HashSet<Cell>();
            for ( int row = 0; row < maze.NumberOfRows; ++row )
            for ( int column = 0; column < maze.NumberOfColumns; ++column )
            {
                result.Add( maze[ row, column ] );
            }

            return result;
        }
    }
}