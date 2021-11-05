namespace Hexagon
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A HexagonGrid allows for storing data arranged such that each element has up to 6 neighbors arranged
    /// hexagonally.
    /// </summary>
    /// <typeparam name="T">The type of data stored in this HexagonGrid.</typeparam>
    public class HexagonGrid<T>
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
            return (Math.Abs(x) + Math.Abs(y)) / 2 <= this.radius;
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
    }
}
