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
        public readonly int Radius;

        private readonly Dictionary<ValueTuple<int,int>, T> grid;

        /// <summary>
        /// A HexagonGrid is indexable.
        /// </summary>
        /// <value>The x and y coordinate to access.</value>
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
        /// 
        /// </summary>
        /// <param name="radius"></param>
        public HexagonGrid(int radius)
        {
            this.Radius = radius;
            this.grid = new ();
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
            return (Math.Abs(x) + Math.Abs(y))/2 <= this.Radius;
        }

        public void Put(int x, int y, T elem)
        {
            if (!this.IsInBounds(x, y))
            {
                throw new IndexOutOfRangeException($"The referenced cell ({x}, {y}) is out of bounds for a Grid with radius: {this.Radius}.");
            }

            this.grid[(x,y)] = elem;
        }

        public T Get(int x, int y)
        {
            if (!this.IsInBounds(x, y))
            {
                throw new IndexOutOfRangeException($"The referenced cell ({x}, {y}) is out of bounds for a Grid with radius: {this.Radius}.");
            }

            T value;
            this.grid.TryGetValue((x,y), out value);
            return value;
        }
    }
}
