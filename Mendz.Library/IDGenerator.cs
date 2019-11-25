using System;
using System.Threading;

namespace Mendz.Library
{
    /// <summary>
    /// Represents an ID generator.
    /// </summary>
    public class IDGenerator
    {
        private readonly object o = new object();
        private volatile int _id = 1;

        /// <summary>
        /// Gets the current ID.
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// Initializes an IDGenerator.
        /// </summary>
        /// <param name="seed">The seed. Default is 1.</param>
        public IDGenerator(int seed = 1) => Seed(seed);

        /// <summary>
        /// Seeds an IDGenerator.
        /// </summary>
        /// <param name="seed">The seed value.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The seed cannot be less than 1.
        /// </exception>
        /// <remarks>
        /// The minimum seed is 1. Thus, the minimum ID is 1.
        /// The ID can only be seeded if the seed is greater than the current ID value.
        /// If the seed is less than or equal to the current ID value, then this method does nothing.
        /// </remarks>
        public void Seed(int seed)
        {
            lock (o)
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                if (seed < 1) throw new ArgumentOutOfRangeException(nameof(seed), "The seed cannot be less than 1.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                if (_id < seed) _id = seed;
            }
        }

        /// <summary>
        /// Generates an ID.
        /// </summary>
        /// <returns>The current ID.</returns>
        public int Generate()
        {
            lock (o)
            {
                return Interlocked.Increment(ref _id);
            }
        }
    }
}
