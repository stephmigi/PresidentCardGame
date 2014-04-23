namespace President.ObjectModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Linq extension methods
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Foreach extension method extending IENumerable
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="enumeration">The IEnumerable to iterate on</param>
        /// <param name="action">Method to apply to each element</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
                action(item);
        }
    }
}
