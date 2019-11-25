using System;

namespace Mendz.Library
{
    /// <summary>
    /// A base class to define a singleton.
    /// </summary>
    /// <remarks>The class of type T must have a parameterless non-public constructor.</remarks>
    /// <typeparam name="T">The type of the singleton class.</typeparam>
    public abstract class SingletonBase<T>
        where T : class
    {
        /// <summary>
        /// Lazy instantiate the class T using its parameterless non-public constructor.
        /// </summary>
        private static readonly Lazy<T> lazy = new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T), true));

#pragma warning disable CA1000 // Do not declare static members on generic types
        /// <summary>
        /// Gets the instance of the class T.
        /// </summary>
        public static T Instance => lazy.Value;
#pragma warning restore CA1000 // Do not declare static members on generic types
    }
}
