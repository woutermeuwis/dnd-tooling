using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterSheet.Core.Extensions
{
    public static class CollectionExtensions
    {

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> toAdd)
        {
            if (collection == null || toAdd == null) return;

            foreach (var item in toAdd)
                collection.Add(item);
        }

        public static void Update<T>(this ICollection<T> collection, IEnumerable<T> toAdd)
        {
            collection?.Clear();
            collection?.AddRange(toAdd);
        }

    }
}
