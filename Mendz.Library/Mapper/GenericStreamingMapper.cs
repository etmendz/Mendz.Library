using System;
using System.Collections.Generic;

namespace Mendz.Library
{
    /// <summary>
    /// Represents a streaming mapper that uses a provided generic mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public class GenericStreamingMapper<TInput, TOutput> : IStreamingMapper<TInput, TOutput>
    {
        /// <summary>
        /// Gets the generic mapper.
        /// </summary>
        public IGenericMapper<TInput, TOutput> GenericMapper { get; private set; }

        /*
        /// <summary>
        /// Creates an instance of a streaming generic mapper.
        /// </summary>
        public GenericStreamingMapper()
        {
            // The caller must set the GenericMapper property.
        }
        */

        /// <summary>
        /// Creates an instance of a streaming generic mapper given the generic mapper.
        /// </summary>
        /// <param name="genericMapper">The generic mapper.</param>
        public GenericStreamingMapper(IGenericMapper<TInput, TOutput> genericMapper) => 
            GenericMapper = genericMapper ?? throw new ArgumentNullException(nameof(genericMapper));

        public IEnumerable<TOutput> Map(IEnumerable<TInput> input, Func<TOutput> instance)
        {
            /*
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            if (GenericMapper == null) throw new InvalidOperationException("The GenericMapper property is null or not set.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            */
            if (input == null) throw new ArgumentNullException(nameof(input));
            foreach (var item in input)
            {
                yield return GenericMapper.Map(item, instance);
            }
        }

#pragma warning disable CA1000 // Do not declare static members on generic types
        /// <summary>
        /// Starts stream mapping inputs to outputs using the provided generic mapper.
        /// </summary>
        /// <param name="input">The enumerable/iterable input.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <param name="genericMapper">The generic mapper.</param>
        /// <returns>The enumerable/iterable output.</returns>
        public static IEnumerable<TOutput> Map(IEnumerable<TInput> input, Func<TOutput> instance, IGenericMapper<TInput, TOutput> genericMapper)
#pragma warning restore CA1000 // Do not declare static members on generic types
        {
            return new GenericStreamingMapper<TInput, TOutput>(genericMapper).Map(input, instance);
        }
    }
}
