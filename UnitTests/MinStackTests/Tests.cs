using System;
using System.Collections.Generic;
using HW01.HashTable;
using HW1.MinStack;
using Xunit;

namespace UnitTests.MinStackTests
{
    public class Tests
    {
        [Fact]
        public void MinValueWorksProperly()
        {
            // Arrange
            var stack = new MinStack<string>();

            // Act & Assert
            stack.Push("caba");
            stack.Push("aba");
            stack.Push("aba");
            Assert.Equal("aba", stack.MinValue());

            stack.Pop();
            Assert.Equal("aba", stack.MinValue());

            stack.Pop();
            Assert.Equal("caba", stack.MinValue());

            Assert.Equal(1, stack.Size);
        }

        [Fact]
        public void PeekWorksProperly()
        {
            // Arrange
            var stack = new MinStack<string>();

            // Act & Assert
            stack.Push("aba");
            stack.Push("caba");
            stack.Push("aba");
            Assert.Equal("aba", stack.Peek());

            stack.Pop();
            Assert.Equal("caba", stack.Peek());

            stack.Pop();
            Assert.Equal("aba", stack.Peek());
        }

        [Fact]
        public void CountIsCalculatedCorrectly()
        {
            // Arrange
            var stack = new MinStack<string>();

            // Act & Assert
            stack.Push("caba");
            stack.Push("aba");
            Assert.Equal(2, stack.Size);
            stack.Pop();

            Assert.Equal(1, stack.Size);
        }

        [Fact]
        public void ClearClearsStack()
        {
            // Arrange
            var stack = new MinStack<string>();

            // Act
            stack.Push("caba");
            stack.Push("aba");
            stack.Push("caba");
            stack.Push("aba");

            // Assert
            Assert.Equal(4, stack.Size);
            stack.Clear();
            Assert.Equal(0, stack.Size);
        }
    }
}
