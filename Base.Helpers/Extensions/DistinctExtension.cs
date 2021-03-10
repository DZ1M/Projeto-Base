using Base.Helpers.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Helpers.Extensions
{
    public static class DistinctExtension
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
       (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, bool> methodEquals,
            Func<TSource, int> methodGetHashCode)
                => source.Distinct(
                    CompareGeneric<TSource>.Create(
                        methodEquals,
                        methodGetHashCode)
                        );
    }
}
