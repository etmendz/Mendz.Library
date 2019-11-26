using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mendz.Library
{
    /// <summary>
    /// The base class of an asynchronous streaming mapper that also implements its asynchronous generic mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public abstract class AsyncStreamingGenericMapperBase<TInput, TOutput> : IAsyncGenericMapper<TInput, TOutput>, IAsyncStreamingMapper<TInput, TOutput>
    {
        public abstract Task<TOutput> MapAsync(TInput input, Func<TOutput> instance);

        public virtual async IAsyncEnumerable<TOutput> MapAsync(IAsyncEnumerable<TInput> input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            await foreach (var item in input)
            {
                yield return await MapAsync(item, instance).ConfigureAwait(false);
            }
        }

        public virtual async IAsyncEnumerable<TOutput> MapAsync(IEnumerable<TInput> input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            foreach (var item in input)
            {
                yield return await MapAsync(item, instance).ConfigureAwait(false);
            }
        }
    }
}
