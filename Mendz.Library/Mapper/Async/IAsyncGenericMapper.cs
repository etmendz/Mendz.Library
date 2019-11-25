using System;
using System.Threading.Tasks;

namespace Mendz.Library
{
    /// <summary>
    /// Defines an asynchronous mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public interface IAsyncGenericMapper<TInput, TOutput>
    {
        /// <summary>
        /// Asynchronously maps the input to the output.
        /// </summary>
        /// <param name="input">The input to map.</param>
        /// <param name="instance">Provides the method to create an instance of TOutput.</param>
        /// <returns>The mapped output.</returns>
        Task<TOutput> MapAsync(TInput input, Func<TOutput> instance);
    }
}
