namespace Hexagon
{
    using NUnit.Framework;
    using System.Collections.Generic;

    public class HexagonGridTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIndexable()
        {
            HexagonGrid<int> grid = new HexagonGrid<int>(2);
            Assert.AreEqual(0, grid[0,0]);
            grid[0,0] = 5;
            Assert.AreEqual(5, grid[0,0]);
        }

        [Test]
        public void TestIterator()
        {
            HexagonGrid<int> grid = new HexagonGrid<int>(2);
            (int, int)[] expected = {
                // r = 0
                ( 0,  0),

                // r = 1
                (-2,  0),
                (-1,  1),
                ( 1,  1),
                ( 2,  0),
                ( 1, -1),
                (-1, -1),

                // r = 2
                (-4,  0),
                (-3,  1),
                (-2,  2),
                ( 0,  2),
                ( 2,  2),
                ( 3,  1),
                ( 4,  0),
                ( 3, -1),
                ( 2, -2),
                ( 0, -2),
                (-2, -2),
                (-3, -1),

                // r = 3
                (-6, 0),
                (-5, 1),
                (-4, 2),
                (-3, 3),
                (-1, 3),
                (1, 3),
                (3, 3),
                (4, 2),
                (5, 1),
                (6, 0),
                (5, -1),
                (4, -2),
                (3, -3),
                (1, -3),
                (-1, -3),
                (-3, -3),
                (-4, -2),
                (-5, -1)
            };

            int ix = 0;
            foreach(((int x, int y), int elem) in grid)
            {
                Assert.AreEqual(expected[ix], (x, y));
                ix++;
            }
            
        }
    }
}