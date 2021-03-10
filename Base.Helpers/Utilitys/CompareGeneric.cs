using System;
using System.Collections.Generic;

namespace Base.Helpers.Utilitys
{
    public class CompareGeneric<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> MethodEquals { get; }
        public Func<T, int> MethodGetHashCode { get; }
        private CompareGeneric(Func<T, T, bool> methodEquals, Func<T, int> methodGetHashCode)
        {
            MethodEquals = methodEquals;
            MethodGetHashCode = methodGetHashCode;
        }
        public static CompareGeneric<T> Create(Func<T, T, bool> methodEquals, Func<T, int> methodGetHashCode)
            => new CompareGeneric<T>(
                 methodEquals,
                 methodGetHashCode
             );
        public bool Equals(T x, T y) => MethodEquals(x, y);
        public int GetHashCode(T obj) => MethodGetHashCode(obj);

    }
}
