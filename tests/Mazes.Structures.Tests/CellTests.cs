using Xunit;

namespace Mazes.Structures.Tests
{
    public class CellTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_Cell()
        {
            Cell cell1 = new Cell( 1, 1 );

            Assert.Equal( 1, cell1.Row );
            Assert.Equal( 1, cell1.Column );

            Cell cell2 = new Cell( 1, 1 );
            Assert.False( cell1.Equals( cell2 ), "A modified Cell equals an unmodified Cell with the same coordinates." );
            Assert.False( cell1.GetHashCode() == cell2.GetHashCode(), $"{nameof(Cell.GetHashCode)}() of two unequal Cells are equal." );
            Assert.True( cell2.Equals( ( object ) cell2 ), $"{nameof(Cell.Equals)}() returns false when comparing Cell to itself." );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_Cell_Linking()
        {
            Cell cell1 = new Cell( 0, 0 );
            cell1.LinkTo( cell1 );
            Assert.False( cell1.IsLinkedTo( cell1 ), "Cell was allowed to link to itself." );

            Cell cell2 = new Cell( 0, 1 );
            cell1.LinkTo( cell2 );
            Assert.True( cell1.IsLinkedTo( cell2 ), "Cell did not link to viable candidate." );

            Cell cell3 = new Cell( 1, 1 );
            Assert.False( cell1.IsLinkedTo( cell3 ), $"Cell is linked to a cell without calling {nameof(Cell.LinkTo)}()." );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_Cell_SetNeighbors()
        {
            Cell cell1 = new Cell( 0, 0 );
            cell1.MakeNeighbors( cell1 );
            Assert.False( cell1.IsNeighborOf( cell1 ), "Cell was allowed to be neighbors with itself." );

            Cell cell2 = new Cell( 0, 1 );
            cell1.MakeNeighbors( cell2 );
            Assert.True( cell1.IsNeighborOf( cell2 ), "Cell did not become neighbors with viable candidate." );

            Cell cell3 = new Cell( 1, 1 );
            Assert.False( cell1.IsNeighborOf( cell3 ), $"Cell is neighbors with a cell without calling {nameof(Cell.IsNeighborOf)}()." );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_Cell_GetNeighbor()
        {
            Cell cell1 = new Cell( 0, 0 );
            Cell cell2 = new Cell( 0, 1 );

            cell1.MakeNeighbors( cell2 );
            Assert.Equal( cell2, cell1.GetNeighbor( cell2.Row, cell2.Column ) );
            Assert.Null( cell1.GetNeighbor( 0, 0 ) );
        }
    }
}