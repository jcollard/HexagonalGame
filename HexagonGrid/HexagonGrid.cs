namespace Hexagon
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A HexagonGrid allows for storing data arranged such that each element has up to 6 neighbors arranged
    /// hexagonally.
    /// </summary>
    /// <typeparam name="T">The type of data stored in this HexagonGrid.</typeparam>
    public class HexagonGrid<T> : IEnumerable<((int, int), T)>
    {
        /// <summary>
        /// The radius of this HexagonGrid.
        /// </summary>
        private readonly int radius;

        /// <summary>
        /// A Dictionary tracking the internal values of the Grid.
        /// </summary>
        private readonly Dictionary<ValueTuple<int, int>, T> grid;

        /// <summary>
        /// Initializes an instance of the HexagonGrid class specifying the radius of the grid.
        /// </summary>
        /// <param name="radius">The radius of the grid.</param>
        public HexagonGrid(int radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException("A HexagonGrid must have a radius of 0 or more.");
            }

            this.radius = radius;
            this.grid = new Dictionary<(int, int), T>();
        }

        /// <summary>
        /// Gets the radius of this HexagonGrid.
        /// </summary>
        public int Radius
        {
            get
            {
                return this.radius;
            }
        }

        /// <summary>
        /// A HexagonGrid is indexable.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <value>The element being placed or retrieved.</value>
        public T this[int x, int y]
        {
            get
            {
                return this.Get(x, y);
            }

            set
            {
                this.Put(x, y, value);
            }
        }

        /// <summary>
        /// Checks if the specified coordinates are within the bounds of this
        /// HexagonGrid.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the bounds of the HexagonGrid and <c>false</c> otherwise.</returns>
        public bool IsInBounds(int x, int y)
        {
            int absSum = Math.Abs(x) + Math.Abs(y);
            if (absSum % 2 == 1)
            {
                return false;
            }

            return (absSum / 2) <= this.radius;
        }

        /// <summary>
        /// Puts the specified element into this HexagonGrid at the specified position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="elem">The element to put.</param>
        private void Put(int x, int y, T elem)
        {
            if (!this.IsInBounds(x, y))
            {
                throw new IndexOutOfRangeException($"The referenced cell ({x}, {y}) is out of bounds for a Grid with radius: {this.radius}.");
            }

            this.grid[(x, y)] = elem;
        }

        /// <summary>
        /// Gets the element at the specified position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The element at the specified position.</returns>
        private T Get(int x, int y)
        {
            if (!this.IsInBounds(x, y))
            {
                throw new IndexOutOfRangeException($"The referenced cell ({x}, {y}) is out of bounds for a Grid with radius: {this.radius}.");
            }

            T value;
            this.grid.TryGetValue((x, y), out value);
            return value;
        }

        /// <summary>
        /// Returns an iterator that returns a pair containing the coordinate and the element contained at that location.
        /// The order of the returned elements starts at (0,0) then returns each surrounding radius starting with the 
        /// element directly to the left and then rotating clock-wise. For example, a HexagonGrid with radius 2 will return
        /// an iterator in the following order:<br/>
        /// radius 0: (0,0) <br/>
        /// radius 1: (-2, 0), (-1, 1), (1, 1), (2, 0), (1, -1) <br/>
        /// radius 2: (-4, 0), (-3, 1), (-2, 2), (0, 2), (2, 2), (3, 1), (4, 0), (3, -1), (2, -2), (0, -2), (-2, -2), (-3, -1)
        /// </summary>
        /// <returns>An IEnumerator for each element in this HexagonGrid.</returns>
        public IEnumerator<((int, int), T)> GetEnumerator()
        {
            yield return ((0, 0), this.Get(0, 0));

            (int, int)[] directions = {
                ( 1,  1), // Up-Right
                ( 2,  0), // Right
                ( 1, -1), // Down-Right
                (-1, -1), // Down-Left
                (-2,  0), // Left
                (-1,  1), // Up-Left
            };

            for (int r = 0; r <= this.radius; r++)
            {
                int x = r * -2;
                int y = 0;
                
                foreach ((int xInc, int yInc) in directions)
                {
                    for (int i = 0; i < r; i++)
                    {
                        yield return ((x, y), this.Get(x, y));
                        x += xInc;
                        y += yInc;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return this.GetEnumerator();
        }
    }
}
