using System;

namespace Mendz.Library
{
    /// <summary>
    /// Defines a mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public interface IGenericMapper<TInput, TOutput>
    {
        /// <summary>
        /// Maps the input to the output.
        /// </summary>
        /// <param name="input">The input to map.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <returns>The mapped output.</returns>
        TOutput Map(TInput input, Func<TOutput> instance);
    }
}
