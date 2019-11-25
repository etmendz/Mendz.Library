using System;
using System.Collections.Generic;

namespace Mendz.Library
{
    /// <summary>
    /// Provides extensions to mappers.
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Maps the input to the output. 
        /// It is assumed that the IGenericMapper implementation can internally create TOutput
        /// without relying on the Func<TOutput> instance, which is passed as null.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="genericMapper">The IGenericMapper instance.</param>
        /// <param name="input">The input to map.</param>
        /// <returns>The mapped output.</returns>
        public static TOutput Map<TInput, TOutput>(this IGenericMapper<TInput, TOutput> genericMapper,
            TInput input)
        {
            if (genericMapper == null) throw new ArgumentNullException(nameof(genericMapper));
            return genericMapper.Map(input, null);
        }

        /// <summary>
        /// Maps a stream of inputs and stream the outputs.
        /// It is assumed that the IStreamingMapper implementation can internally create TOutput
        /// without relying on the Func<TOutput> instance, which is passed as null.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="streamingMapper">The IStreamingMapper instance.</param>
        /// <param name="input">The input to map.</param>
        /// <returns>The mapped output.</returns>
        public static IEnumerable<TOutput> Map<TInput, TOutput>(this IStreamingMapper<TInput, TOutput> streamingMapper,
            IEnumerable<TInput> input)
        {
            if (streamingMapper == null) throw new ArgumentNullException(nameof(streamingMapper));
            return streamingMapper.Map(input, null);
        }
    }
}
