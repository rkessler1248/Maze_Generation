using Xunit;

namespace Mazes.Structures.Tests
{
    public class CellTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Cell_Construction()
        {
            Coordinate coordinate = new Coordinate( 1, 2 );
            Cell cell1 = new Cell( coordinate );

            Assert.Equal( 1, cell1.Column );
            Assert.Equal( 2, cell1.Row );

            Assert.NotNull( cell1.Links );
            Assert.Empty( cell1.Links );

            Assert.NotNull( cell1.Neighbors );
            Assert.Empty( cell1.Neighbors );

            Cell cell2 = new Cell( 1, 2 );
            Assert.Equal( 1, cell2.Column );
            Assert.Equal( 2, cell2.Row );

            Assert.NotNull( cell2.Links );
            Assert.Empty( cell2.Links );

            Assert.NotNull( cell2.Neighbors );
            Assert.Empty( cell2.Neighbors );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Cell_SetNeighbor()
        {
            Cell cell1 = new Cell( 0, 0 );
            cell1.SetNeighbor( cell1 );

            Assert.Empty( cell1.Neighbors );

            Cell cell2 = new Cell( 0, 0 );
            cell1.SetNeighbor( cell2 );

            Assert.Empty( cell1.Neighbors );
            Assert.Empty( cell2.Neighbors );

            Cell cell3 = new Cell( 0, 1 );
            cell1.SetNeighbor( cell3 );

            Assert.NotEmpty( cell1.Neighbors );
            Assert.NotEmpty( cell3.Neighbors );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Cell_IsNeighbor()
        {
            Cell cell1 = new Cell( 0, 0 );
            Assert.False( cell1.IsNeighbor( cell1 ) );

            Cell cell2 = new Cell( 0, 1 );
            Assert.False( cell1.IsNeighbor( cell2 ) );
            Assert.False( cell2.IsNeighbor( cell1 ) );

            cell1.SetNeighbor( cell2 );
            Assert.True( cell1.IsNeighbor( cell2 ) );
            Assert.True( cell2.IsNeighbor( cell1 ) );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Cell_LinkTo()
        {
            Cell cell1 = new Cell( 0, 0 );
            cell1.LinkTo( cell1 );

            Assert.Empty( cell1.Links );

            Cell cell2 = new Cell( 0, 0 );
            cell1.LinkTo( cell2 );

            Assert.Empty( cell1.Links );
            Assert.Empty( cell2.Links );

            Cell cell3 = new Cell( 0, 1 );
            cell1.LinkTo( cell3 );

            Assert.NotEmpty( cell1.Links );
            Assert.NotEmpty( cell3.Links );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Cell_IsLinkedTo()
        {
            Cell cell1 = new Cell( 0, 0 );
            Assert.False( cell1.IsLinkedTo( cell1 ) );

            Cell cell2 = new Cell( 0, 1 );
            Assert.False( cell1.IsLinkedTo( cell2 ) );
            Assert.False( cell2.IsLinkedTo( cell1 ) );

            cell1.LinkTo( cell2 );
            Assert.True( cell1.IsLinkedTo( cell2 ) );
            Assert.True( cell2.IsLinkedTo( cell1 ) );
        }
    }
}