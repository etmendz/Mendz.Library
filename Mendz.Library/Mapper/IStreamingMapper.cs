using System;
using System.Collections.Generic;

namespace Mendz.Library
{
    /// <summary>
    /// Defines a streaming mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public interface IStreamingMapper<TInput, TOutput>
    {
        /// <summary>
        /// Maps a stream of inputs and stream the outputs.
        /// </summary>
        /// <param name="input">The input to map.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <returns>Enumerable/iterable output.</returns>
        IEnumerable<TOutput> Map(IEnumerable<TInput> input, Func<TOutput> instance);
    }
}
