namespace Mazes.Structures
{
    public readonly struct Maze
    {
        public readonly int Rows;
        public readonly int Columns;

        private readonly Cell[ , ] _cells;

        public Maze( int rows, int columns, Cell[ , ] cells )
        {
            Rows = rows;
            Columns = columns;
            _cells = cells;
        }

        public ref Cell this[ int row, int column ] => ref _cells[ row, column ];
    }
}