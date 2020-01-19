using System;
using System.Collections.Generic;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// A convention-based mapper that maps the indexed input key values to the indexed output key values.
    /// </summary>
    /// <typeparam name="TInput">The indexed input type.</typeparam>
    /// <typeparam name="TOutput">The indexed output type.</typeparam>
    /// <remarks>
    /// Use PropertyNames to limit which property names will be mapped. The matching is case-sensitive.
    /// Default cross-type assignment behaviors and compatibility rules apply.
    /// </remarks>
    public class StreamingKeyValueMapper<TInput, TOutput> : StreamingGenericMapperBase<TInput, TOutput>
    {
        /// <summary>
        /// Stores the delegate that will set the indexed input key values to the indexed output key values.
        /// </summary>
        /// <remarks>
        /// You can also use the handler to perform custom conversion and mapping operations based on the key's value.
        /// This is useful when converting between data types or when property names differ.
        /// </remarks>
        /// <example>
        /// public void SetKeyValueHandler(output, key, input) => output[key] = input[key];
        /// </example>
        private readonly Action<TOutput, string, TInput> _handler = null;

        /// <summary>
        /// Gets the list of property names to map. If empty, all matching property names will be mapped.
        /// </summary>
        public List<string> PropertyNames { get; private set;  }

        /// <summary>
        /// Creates a streaming key value mapper given the output key value handler.
        /// </summary>
        /// <param name="handler">The delegate that will set the indexed output key value.</param>
        /// <param name="propertyNames">The list of property names to map.</param>
        public StreamingKeyValueMapper(Action<TOutput, string, TInput> handler, List<string> propertyNames)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            PropertyNames = propertyNames ?? throw new ArgumentNullException(nameof(propertyNames));
        }

        public override TOutput Map(TInput input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            TOutput output = instance();
            if (PropertyNames.Count > 0)
            {
                foreach (var propertyName in PropertyNames)
                {
                    _handler(output, propertyName, input);
                }
            }
            return output;
        }
    }
}
