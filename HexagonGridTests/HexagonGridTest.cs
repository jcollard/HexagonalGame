using NUnit.Framework;

namespace Hexagon
{
    public class HexagonGridTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            HexagonGrid<int> grid = new HexagonGrid<int>(2);
            Assert.AreEqual(0, grid[0,0]);
            grid[0,0] = 5;
            Assert.AreEqual(5, grid[0,0]);
        }
    }
}