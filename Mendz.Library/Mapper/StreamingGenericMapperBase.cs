using System;
using System.Collections.Generic;

namespace Mendz.Library
{
    /// <summary>
    /// The base class of a streaming mapper that also implements its generic mapper.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public abstract class StreamingGenericMapperBase<TInput, TOutput> : IGenericMapper<TInput, TOutput>, IStreamingMapper<TInput, TOutput>
    {
        public abstract TOutput Map(TInput input, Func<TOutput> instance);

        public virtual IEnumerable<TOutput> Map(IEnumerable<TInput> input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            foreach (var item in input)
            {
                yield return Map(item, instance);
            }
        }
    }
}
