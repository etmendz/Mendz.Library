using System;
using System.Collections.Generic;

namespace Mendz.Library
{
    /// <summary>
    /// Defines an asynchronous streaming mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public interface IAsyncStreamingMapper<TInput, TOutput>
    {
        /// <summary>
        /// Asynchronously maps a stream of inputs and stream the outputs.
        /// </summary>
        /// <param name="input">The input to map.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <returns>Asynchronously enumerable/iterable output.</returns>
        IAsyncEnumerable<TOutput> MapAsync(IAsyncEnumerable<TInput> input, Func<TOutput> instance);

        /// <summary>
        /// Asynchronously maps a stream of inputs and stream the outputs.
        /// </summary>
        /// <param name="input">The input to map.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <returns>Asynchronously enumerable/iterable output.</returns>
        IAsyncEnumerable<TOutput> MapAsync(IEnumerable<TInput> input, Func<TOutput> instance);
    }
}
