using System;
using System.Collections.Generic;
using System.Linq;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// A convention-based mapper that uses System.Reflection to map the input's properties to the indexed output key values.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The indexed output type.</typeparam>
    /// <remarks>
    /// The input's property names (case-sensitive) are used as keys to map values to the indexed output.
    /// Example, if the output type has an indexer, the handler can be a method that sets output[key] = value. 
    /// Use PropertyNames to limit which property names will be mapped.
    /// Default cross-type assignment behaviors and compatibility rules apply.
    /// </remarks>
    public class StreamingToKeyValueMapper<TInput, TOutput> : StreamingGenericMapperBase<TInput, TOutput>
    {
        /// <summary>
        /// Stores the delegate that will set the input property value to the indexed output key values.
        /// </summary>
        /// <remarks>
        /// You can also use the handler to perform custom conversion and mapping operations based on the key's value.
        /// This is useful when converting between data types or when property names differ.
        /// </remarks>
        /// <example>
        /// public void SetOutputKeyValueHandler(output, key, value) => output[key] = value;
        /// </example>
        private readonly Action<TOutput, string, object> _handler = null;

        /// <summary>
        /// Gets the list of property names to map. If empty, all matching property names will be mapped.
        /// </summary>
        public List<string> PropertyNames { get; } = new List<string>();

        /// <summary>
        /// Creates a streaming key value mapper given the output key value handler.
        /// </summary>
        /// <param name="handler">The delegate that will set the indexed output key value.</param>
        public StreamingToKeyValueMapper(Action<TOutput, string, object> handler) =>
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        public override TOutput Map(TInput input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            TOutput output = instance();
            var properties = from i in input.GetType().GetProperties()
                             select i;
            if (PropertyNames.Count > 0) properties = StreamingPropertyMapper<TInput, TOutput>.FilterPropertyNames(properties, PropertyNames);
            foreach (var inputProperty in properties)
            {
                _handler(output, inputProperty.Name, inputProperty.GetValue(input));
            }
            return output;
        }
    }
}
