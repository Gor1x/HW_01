using System.Collections.Generic;

namespace HW01.HashTable
{
    public interface IHashTable<T> : IEnumerable<T>
    {
        bool Add(T value);
        bool Contains(T value);
        bool Remove(T value);
    }
}
