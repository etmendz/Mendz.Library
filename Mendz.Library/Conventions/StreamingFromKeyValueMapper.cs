using System;
using System.Collections.Generic;
using System.Linq;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// A convention-based mapper that uses System.Reflection to map indexed input key values to the output's properties.
    /// </summary>
    /// <typeparam name="TInput">The indexed input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    /// <remarks>
    /// The output's property names (case-sensitive) are used as keys to map values from the indexed input.
    /// Example, if the input type has an indexer, the handler can be a method that returns input[key]. 
    /// Use PropertyNames to limit which property names will be mapped.
    /// Default cross-type assignment behaviors and compatibility rules apply.
    /// </remarks>
    public class StreamingFromKeyValueMapper<TInput, TOutput> : StreamingGenericMapperBase<TInput, TOutput>
    {
        /// <summary>
        /// Stores the delegate that will return the indexed input key value.
        /// </summary>
        /// <remarks>
        /// You can also use the handler to perform custom conversion and mapping operations based on the key's value.
        /// This is useful when converting between data types or when property/key names differ.
        /// </remarks>
        /// <example>
        /// public object GetInputKeyValueHandler(input, key) => input[key];
        /// </example>
        private readonly Func<TInput, string, object> _handler = null;

        /// <summary>
        /// Gets the list of property names to map. If empty, all matching property names will be mapped.
        /// </summary>
        public List<string> PropertyNames { get; } = new List<string>();

        /// <summary>
        /// Creates a streaming key value mapper given the input key value handler.
        /// </summary>
        /// <param name="handler">The delegate that will get the indexed input key value.</param>
        public StreamingFromKeyValueMapper(Func<TInput, string, object> handler) =>
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        public override TOutput Map(TInput input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            TOutput output = instance();
            var properties = from o in output.GetType().GetProperties()
                             select o;
            if (PropertyNames.Count > 0) properties = StreamingPropertyMapper<TInput, TOutput>.FilterPropertyNames(properties, PropertyNames);
            foreach (var outputProperty in properties)
            {
                outputProperty.SetValue(output, _handler(input, outputProperty.Name));
            }
            return output;
        }
    }
}
