using System;
using System.Collections.Generic;

namespace Mendz.Library
{
    /// <summary>
    /// Represents an asynchronous streaming mapper that uses a provided asynchronous generic mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public class AsyncGenericStreamingMapper<TInput, TOutput> : IAsyncStreamingMapper<TInput, TOutput>
    {
        /// <summary>
        /// Gets the asynchronous generic mapper.
        /// </summary>
        public IAsyncGenericMapper<TInput, TOutput> GenericMapper { get; private set; }

        /*
        /// <summary>
        /// Creates an instance of an asynchronous streaming generic mapper.
        /// </summary>
        public AsyncGenericStreamingMapper()
        {
            // The caller must set the GenericMapper property.
        }
        */

        /// <summary>
        /// Creates an instance of an asynchronous streaming generic mapper given the generic mapper.
        /// </summary>
        /// <param name="genericMapper">The asynchronous generic mapper.</param>
        public AsyncGenericStreamingMapper(IAsyncGenericMapper<TInput, TOutput> genericMapper) =>
            GenericMapper = genericMapper ?? throw new ArgumentNullException(nameof(genericMapper));

        public async IAsyncEnumerable<TOutput> MapAsync(IAsyncEnumerable<TInput> input, Func<TOutput> instance)
        {
            /*
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            if (GenericMapper == null) throw new InvalidOperationException("The GenericMapper property is null or not set.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            */
            if (input == null) throw new ArgumentNullException(nameof(input));
            await foreach (var item in input)
            {
                yield return await GenericMapper.MapAsync(item, instance).ConfigureAwait(false);
            }
        }

        public async IAsyncEnumerable<TOutput> MapAsync(IEnumerable<TInput> input, Func<TOutput> instance)
        {
            /*
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            if (GenericMapper == null) throw new InvalidOperationException("The GenericMapper property is null or not set.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            */
            if (input == null) throw new ArgumentNullException(nameof(input));
            foreach (var item in input)
            {
                yield return await GenericMapper.MapAsync(item, instance).ConfigureAwait(false);
            }
        }

#pragma warning disable CA1000 // Do not declare static members on generic types
        /// <summary>
        /// Asynchronously starts stream mapping inputs to outputs using the provided generic mapper.
        /// </summary>
        /// <param name="input">The enumerable/iterable input.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <param name="genericMapper">The asynchronous generic mapper.</param>
        /// <returns>The asynchronously enumerable/iterable output.</returns>
        public static async IAsyncEnumerable<TOutput> MapAsync(IAsyncEnumerable<TInput> input, Func<TOutput> instance, IAsyncGenericMapper<TInput, TOutput> genericMapper)
#pragma warning restore CA1000 // Do not declare static members on generic types
        {
            await foreach (var item in new AsyncGenericStreamingMapper<TInput, TOutput>(genericMapper).MapAsync(input, instance))
            {
                yield return item;
            }
        }

#pragma warning disable CA1000 // Do not declare static members on generic types
        /// <summary>
        /// Asynchronously starts stream mapping inputs to outputs using the provided generic mapper.
        /// </summary>
        /// <param name="input">The enumerable/iterable input.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <param name="genericMapper">The asynchronous generic mapper.</param>
        /// <returns>The asynchronously enumerable/iterable output.</returns>
        public static async IAsyncEnumerable<TOutput> MapAsync(IEnumerable<TInput> input, Func<TOutput> instance, IAsyncGenericMapper<TInput, TOutput> genericMapper)
#pragma warning restore CA1000 // Do not declare static members on generic types
        {
            await foreach (var item in new AsyncGenericStreamingMapper<TInput, TOutput>(genericMapper).MapAsync(input, instance))
            {
                yield return item;
            }
        }
    }
}
