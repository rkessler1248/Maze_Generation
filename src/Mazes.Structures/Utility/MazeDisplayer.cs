using System.Linq;
using System.Text;

namespace Mazes.Structures.Utility
{
    public class MazeDisplayer
    {
        public string AsString( Maze maze )
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine( $"*{string.Join( "*", Enumerable.Repeat( "---", maze.NumberOfColumns ) )}*" );

            for ( int row = 0; row < maze.NumberOfRows; ++row )
            {
                StringBuilder top = new StringBuilder();
                StringBuilder bottom = new StringBuilder();

                top.Append( "|" );
                bottom.Append( "*" );
                for ( int column = 0; column < maze.NumberOfColumns; ++column )
                {
                    string easternBoundary = column < maze.NumberOfColumns - 1 && maze[ row, column ].IsLinkedTo( maze[ row, column + 1 ] ) ? " " : "|";
                    top.Append( $"   {easternBoundary}" );

                    string southernBoundary = row < maze.NumberOfRows - 1 && maze[ row, column ].IsLinkedTo( maze[ row + 1, column ] ) ? "   " : "---";
                    bottom.Append( $"{southernBoundary}*" );
                }

                result.AppendLine( top.ToString() );
                result.AppendLine( bottom.ToString() );
            }

            return result.ToString();
        }
    }
}