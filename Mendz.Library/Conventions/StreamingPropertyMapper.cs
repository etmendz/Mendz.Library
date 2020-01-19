using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mendz.Library.Conventions
{
    /// <summary>
    /// A convention-based mapper that uses System.Reflection to map matching properties in the input type to the output type.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    /// <remarks>
    /// The properties are matched by name (case-sensitive) and type. If both match, the input value is copied to the output.
    /// Use PropertyNames to limit which property names will be mapped.
    /// Default cross-type assignment behaviors and compatibility rules apply.
    /// </remarks>
    public class StreamingPropertyMapper<TInput, TOutput> : StreamingGenericMapperBase<TInput, TOutput>
    {
        /// <summary>
        /// Stores the delegate that will return the input property value.
        /// </summary>
        /// <remarks>
        /// You can also use the handler to perform custom conversion and mapping operations based on the property name.
        /// This is useful when converting between data types or when property names differ.
        /// </remarks>
        private readonly Func<TInput, string, object> _handler = null;

        /// <summary>
        /// Gets the list of property names to map. If empty, all matching property names will be mapped.
        /// </summary>
        public List<string> PropertyNames { get; } = new List<string>();

        public StreamingPropertyMapper() { }

        /// <summary>
        /// Creates a streaming key value mapper given the input property handler.
        /// </summary>
        /// <param name="handler">The delegate that will return the input property value.</param>
        public StreamingPropertyMapper(Func<TInput, string, object> handler) =>
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        public override TOutput Map(TInput input, Func<TOutput> instance)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            TOutput output = instance();
            var outputType = output.GetType();
            var properties = from i in input.GetType().GetProperties()
                             join o in outputType.GetProperties() 
                                on new { i.Name, i.PropertyType } equals new { o.Name, o.PropertyType }
                             select i;
            if (PropertyNames.Count > 0) properties = StreamingPropertyMapper<TInput, TOutput>.FilterPropertyNames(properties, PropertyNames);
            string propertyName;
            foreach (var inputProperty in properties)
            {
                propertyName = inputProperty.Name;
                object value;
                if (_handler == null)
                {
                    value = inputProperty.GetValue(input);
                }
                else
                {
                    value = _handler(input, propertyName);
                }
                outputType.GetProperty(propertyName).SetValue(output, value);
            }
            return output;
        }

        internal static IEnumerable<PropertyInfo> FilterPropertyNames(IEnumerable<PropertyInfo> properties, List<string> propertyNames)
        {
            return from p in properties.ToList()
                   join n in propertyNames on p.Name equals n
                   select p;
        }
    }
}
