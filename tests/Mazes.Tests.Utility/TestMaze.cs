using Mazes.Structures;

namespace Mazes.Tests.Utility
{
    public static class TestMaze
    {
        // Test Maze Visualization
        /* ============================== *
         *                                *
         *     *---*---*---*---*---*      *
         *     |                   |      *
         *     *---*   *---*---*   *      *
         *     |           |   |   |      *
         *     *   *   *---*   *   *      *
         *     |   |   |   |       |      *
         *     *---*   *   *---*---*      *
         *     |                   |      *
         *     *   *---*---*   *   *      *
         *     |       |       |   |      *
         *     *---*---*---*---*---*      *
         *                                *
         * ============================== */
        public static Maze Generate()
        {
            Maze maze = new Maze( 5, 5 );

            // First row horizontal
            maze[ 0, 0 ].LinkTo( maze[ 0, 1 ] );
            maze[ 0, 1 ].LinkTo( maze[ 0, 2 ] );
            maze[ 0, 2 ].LinkTo( maze[ 0, 3 ] );
            maze[ 0, 3 ].LinkTo( maze[ 0, 4 ] );

            // First row vertical
            maze[ 0, 1 ].LinkTo( maze[ 1, 1 ] );
            maze[ 0, 4 ].LinkTo( maze[ 1, 4 ] );

            // Second row horizontal
            maze[ 1, 0 ].LinkTo( maze[ 1, 1 ] );
            maze[ 1, 1 ].LinkTo( maze[ 1, 2 ] );

            // Second Row vertical
            maze[ 1, 0 ].LinkTo( maze[ 2, 0 ] );
            maze[ 1, 1 ].LinkTo( maze[ 2, 1 ] );
            maze[ 1, 3 ].LinkTo( maze[ 2, 3 ] );
            maze[ 1, 4 ].LinkTo( maze[ 2, 4 ] );

            maze[ 2, 3 ].LinkTo( maze[ 2, 4 ] );

            maze[ 2, 1 ].LinkTo( maze[ 3, 1 ] );
            maze[ 2, 2 ].LinkTo( maze[ 3, 2 ] );

            maze[ 3, 0 ].LinkTo( maze[ 3, 1 ] );
            maze[ 3, 1 ].LinkTo( maze[ 3, 2 ] );
            maze[ 3, 2 ].LinkTo( maze[ 3, 3 ] );
            maze[ 3, 3 ].LinkTo( maze[ 3, 4 ] );

            maze[ 3, 0 ].LinkTo( maze[ 4, 0 ] );
            maze[ 3, 3 ].LinkTo( maze[ 4, 3 ] );
            maze[ 3, 4 ].LinkTo( maze[ 4, 4 ] );

            maze[ 4, 0 ].LinkTo( maze[ 4, 1 ] );
            maze[ 4, 2 ].LinkTo( maze[ 4, 3 ] );

            return maze;
        }
    }
}