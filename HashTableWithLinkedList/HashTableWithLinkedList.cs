using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HW01.HashTable
{
    public class HashTableWithLinkedList<T> : IHashTable<T>
    {
        private const int MinCapacity = 8;
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        private LinkedList<T>[] Data { get; set; }

        public HashTableWithLinkedList() : this(capacity: MinCapacity)
        {
        }

        private HashTableWithLinkedList(int capacity)
        {
            Capacity = capacity;
            Data = new LinkedList<T>[Capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.Where(list => list != null).SelectMany(list => list).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetBucketIndex(T value)
        {
            var index = value.GetHashCode() % Capacity;
            if (index < 0)
                index += Capacity;
            return index;
        }
        public bool Add(T value)
        {
            if (Count * 4 >= 3 * Data.Length) // loadFactor = Size / Buckets <= 3/4
            {
                Rebuild(Capacity * 2);
            }
            var bucketIndex = GetBucketIndex(value);
            if (Data[bucketIndex] == null)
            {
                Data[bucketIndex] = new LinkedList<T>();
            }
            Data[bucketIndex].AddLast(value);
            Count++;
            return true;
        }
        private void Rebuild(int newCapacity)
        {
            Capacity = newCapacity;
            var allTableElements = new List<T>(this);

            Count = 0;
            Data = new LinkedList<T>[Capacity];
            foreach (var tableElement in allTableElements)
            {
                Add(tableElement);
            }
        }

        public bool Contains(T value)
        {
            var bucketIndex = GetBucketIndex(value);
            return Data[bucketIndex] != null && Data[bucketIndex].Contains(value);
        }

        public bool Remove(T value)
        {
            var bucketIndex = GetBucketIndex(value);
            if (Data[bucketIndex] == null)
                return false;
            var removeResult = Data[bucketIndex].Remove(value);
            if (removeResult)
                Count--;
            // If size is too small in comparison with capacity
            // We should decrease amount of buckets
            if (8 * Count <= Capacity && Capacity >= MinCapacity * 2)
                Rebuild(Capacity / 2);
            return removeResult;
        }
    }
}
