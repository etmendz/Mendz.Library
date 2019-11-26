using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mendz.Library
{
    /// <summary>
    /// Provides extensions to asynchronous mappers.
    /// </summary>
    public static class AsyncMapperExtensions
    {
        /// <summary>
        /// Asynchronously maps the input to the output. 
        /// It is assumed that the IAsyncGenericMapper implementation can internally create TOutput
        /// without relying on the Func<TOutput> instance, which is passed as null.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="genericMapper">The IAsyncGenericMapper instance.</param>
        /// <param name="input">The input to map.</param>
        /// <returns>The mapped output.</returns>
        public static async Task<TOutput> MapAsync<TInput, TOutput>(this IAsyncGenericMapper<TInput, TOutput> genericMapper,
            TInput input)
        {
            if (genericMapper == null) throw new ArgumentNullException(nameof(genericMapper));
            return await genericMapper.MapAsync(input, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously maps a stream of inputs and stream the outputs.
        /// It is assumed that the IAsyncStreamingMapper implementation can internally create TOutput
        /// without relying on the Func<TOutput> instance, which is passed as null.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="streamingMapper">The IAsyncStreamingMapper instance.</param>
        /// <param name="input">The input to map.</param>
        /// <returns>The mapped output.</returns>
        public static async IAsyncEnumerable<TOutput> MapAsync<TInput, TOutput>(this IAsyncStreamingMapper<TInput, TOutput> streamingMapper,
            IAsyncEnumerable<TInput> input)
        {
            if (streamingMapper == null) throw new ArgumentNullException(nameof(streamingMapper));
            await foreach (var item in streamingMapper.MapAsync(input, null))
            {
                yield return item;
            }
        }

        /// <summary>
        /// Asynchronously maps a stream of inputs and stream the outputs.
        /// It is assumed that the IAsyncStreamingMapper implementation can internally create TOutput
        /// without relying on the Func<TOutput> instance, which is passed as null.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="streamingMapper">The IAsyncStreamingMapper instance.</param>
        /// <param name="input">The input to map.</param>
        /// <returns>The mapped output.</returns>
        public static async IAsyncEnumerable<TOutput> MapAsync<TInput, TOutput>(this IAsyncStreamingMapper<TInput, TOutput> streamingMapper,
            IEnumerable<TInput> input)
        {
            if (streamingMapper == null) throw new ArgumentNullException(nameof(streamingMapper));
            await foreach (var item in streamingMapper.MapAsync(input, null))
            {
                yield return item;
            }
        }
    }
}
