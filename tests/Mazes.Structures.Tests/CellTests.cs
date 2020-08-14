using Xunit;

namespace Mazes.Structures.Tests
{
    public class CellTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_ProtoCell()
        {
            var cell = new ProtoCell( 1, 1 );

            Assert.Equal( 1, cell.Row );
            Assert.Equal( 1, cell.Column );

            Assert.Equal( 0, cell.Neighbors.Count );
            Assert.Equal( 0, cell.Links.Count );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_2D_Orthogonal_ProtoCell_Linking()
        {
            var cell1 = new ProtoCell( 0, 0 );
            cell1.LinkTo( cell1 );
            Assert.False( cell1.IsLinkedTo( cell1 ) );

            var cell2 = new ProtoCell( 0, 0 );
            cell1.LinkTo( cell2 );
            Assert.False( cell1.IsLinkedTo( cell2 ) );

            var cell3 = new ProtoCell( 0, 1 );
            cell1.LinkTo( cell3 );

            Assert.True( cell1.IsLinkedTo( cell3 ) );

            var cell4 = new ProtoCell( 1, 1 );
            Assert.False( cell1.IsLinkedTo( cell4 ) );
        }
    }
}