using Xunit;

namespace Mazes.Structures.Tests
{
    public class CoordinateTests
    {
        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Coordinate_Construction()
        {
            Coordinate coordinate1 = new Coordinate( 1, 2 );
            Assert.Equal( 1, coordinate1[ 0 ] );
            Assert.Equal( 2, coordinate1[ 1 ] );
            Assert.Equal( 2, coordinate1.Dimensions );

            Coordinate coordinate2 = new Coordinate( 1, 2, 3 );
            Assert.Equal( 1, coordinate2[ 0 ] );
            Assert.Equal( 2, coordinate2[ 1 ] );
            Assert.Equal( 3, coordinate2[ 2 ] );
            Assert.Equal( 3, coordinate2.Dimensions );
        }

        [Fact]
        [Trait( "Category", "Unit" )]
        public void Test_Coordinate_Equality()
        {
            Coordinate coordinate1 = new Coordinate( 1, 2 );
            Coordinate coordinate2 = new Coordinate( 1, 2 );

            Assert.Equal( coordinate1, coordinate1 );
            Assert.Equal( coordinate1, coordinate2 );

            Assert.True( coordinate1 == coordinate2 );
            Assert.False( coordinate1 != coordinate2 );
            Assert.Equal( coordinate1.GetHashCode(), coordinate2.GetHashCode() );

            Coordinate coordinate3 = new Coordinate( 3, 4 );
            Assert.NotEqual( coordinate1, coordinate3 );
            Assert.False( coordinate1 == coordinate3 );
            Assert.True( coordinate1 != coordinate3 );
            Assert.NotEqual( coordinate1.GetHashCode(), coordinate3.GetHashCode() );

            Coordinate coordinate4 = new Coordinate( 1, 2, 3 );
            Assert.NotEqual( coordinate1, coordinate4 );

            Assert.NotEqual( coordinate1, ( object ) 1 );
        }
    }
}