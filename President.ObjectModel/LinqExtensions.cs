using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.ObjectModel
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Foreach extension method extending IENumerable
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="enumeration">The ienumerable to iterate on</param>
        /// <param name="action">Method to apply to each element</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
                action(item);
        }
    }
}
