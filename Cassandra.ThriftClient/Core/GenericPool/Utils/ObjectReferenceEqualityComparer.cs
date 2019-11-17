﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SkbKontur.Cassandra.ThriftClient.Core.GenericPool.Utils
{
    internal class ObjectReferenceEqualityComparer<T> : EqualityComparer<T> where T : class
    {
        public override bool Equals(T x, T y)
        {
            return ReferenceEquals(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        public new static IEqualityComparer<T> Default => defaultComparer ?? (defaultComparer = new ObjectReferenceEqualityComparer<T>());
        private static IEqualityComparer<T> defaultComparer;
    }
}