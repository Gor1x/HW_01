using System;
using System.Collections.Generic;
using HW01.HashTable;
using Xunit;


namespace UnitTests.HashTableTests
{
    public class Tests
    {
        private const int NumberConstant = 3;
        private const string StringConstant1 = "Kek";
        private const string StringConstant2 = "LOL";
        private const string StringConstant3 = "randomString3";
        private const int MinCapacity = 8;

        [Fact]
        public void OneAddWorksProperly()
        {
            // Arrange 
            var hashTable = new HashTableWithLinkedList<int>();

            // Act
            hashTable.Add(NumberConstant);

            // Assert 
            Assert.True(hashTable.Contains(NumberConstant));
            Assert.Equal(1, hashTable.Count);
        }

        [Fact]
        public void ContainsWithStringsWorksProperly()
        {
            // Arrange 
            var hashTable = new HashTableWithLinkedList<string>();

            // Act
            hashTable.Add(StringConstant1);
            hashTable.Add(StringConstant2);

            // Assert 
            Assert.True(hashTable.Contains(StringConstant1));
            Assert.True(hashTable.Contains(StringConstant2));
            Assert.False(hashTable.Contains(StringConstant3));
        }

        [Fact]
        public void AddRemoveWorksProperly()
        {
            // Arrange 
            var hashTable = new HashTableWithLinkedList<int>();

            // Act
            hashTable.Add(NumberConstant);
            hashTable.Remove(NumberConstant);

            // Assert 
            Assert.False(hashTable.Contains(NumberConstant));
            Assert.Equal(0, hashTable.Count);
            Assert.Equal(MinCapacity, hashTable.Capacity);
        }

        [Fact]
        public void CapacityIncreasesWhenLoadFactorIsBig()
        {
            const int maxSizeBeforeCapacityIncrease = 3 * MinCapacity / 4;
            // Arrange 
            var hashTable = new HashTableWithLinkedList<int>();

            // Act & Assert
            while (hashTable.Count < maxSizeBeforeCapacityIncrease)
            {
                hashTable.Add(NumberConstant);
                Assert.Equal(MinCapacity, hashTable.Capacity);
            }

            hashTable.Add(NumberConstant);
            Assert.Equal(MinCapacity * 2, hashTable.Capacity);
        }

        [Fact]
        public void CapacityDecreasesWhenLoadFactorIsLow()
        {
            const int maxSizeBeforeCapacityIncrease = 3 * MinCapacity / 4;
            const int minSizeBeforeRebuild = MinCapacity * 2 / 8;
            // Arrange 
            var hashTable = new HashTableWithLinkedList<int>();

            // Act & Assert
            while (hashTable.Count <= maxSizeBeforeCapacityIncrease + 1)
            {
                hashTable.Add(NumberConstant);
            }
            Assert.Equal(MinCapacity * 2, hashTable.Capacity);

            while (hashTable.Count > minSizeBeforeRebuild + 1)
            {
                hashTable.Remove(NumberConstant);
                Assert.Equal(MinCapacity * 2, hashTable.Capacity);
            }
            hashTable.Remove(NumberConstant);
            Assert.Equal(MinCapacity, hashTable.Capacity);
        }

        [Fact]
        public void HashTableOnInitHasProperSizeAndCapacity()
        {
            // Arrange
            var hashTable = new HashTableWithLinkedList<int>();

            // Assert
            Assert.Equal(0, hashTable.Count);
            Assert.Equal(MinCapacity, hashTable.Capacity);
        }

        [Fact]
        public void EnumerationWorksProperly()
        {
            // Arrange
            var hashTable = new HashTableWithLinkedList<string>();
            var hashTableData = new List<string>
            {
                StringConstant1,
                StringConstant2
            };

            // Act
            foreach (var item in hashTableData)
            {
                hashTable.Add(item);
            }

            // Assert
            Assert.Equal(hashTable.Count, hashTableData.Count);
            foreach (var item in hashTable)
            {
                Assert.True(hashTableData.Contains(item));
            }
        }
    }
}
