using System.Collections.Generic;
using System.Linq;
using HW1.PasswordGenerator;
using Xunit;

namespace UnitTests.PasswordGeneratorTests
{
    public class Tests
    {
        private static bool IsUpper(char c) => c >= 'A' && c <= 'Z';

        private static bool IsNumber(char c) => c >= '0' && c <= '9';

        [Fact]
        public void PasswordsStressTest()
        {
            // Arrange 
            var generator = new PasswordGenerator(seed: 0);

            // Act
            var passwords = Enumerable.Range(0, 100).Select(unused => generator.Generate()).ToList();

            // Assert 
            Assert.Equal(passwords.Count, passwords.ToHashSet().Count);
            foreach (var pass in passwords)
            {
                Assert.InRange(pass.Length, 6, 20);
                Assert.Equal(1, pass.Count(c => c == '_'));
                Assert.InRange(pass.Where(IsUpper).Count(), 2, 20);
                Assert.InRange(pass.Where(IsNumber).Count(), 0, 5);
                for (var i = 0; i + 1 < pass.Length; i++)
                {
                    Assert.False(IsNumber(pass[i]) && IsNumber(pass[i + 1]));
                }
            }
        }

        [Fact]
        public void GenerateWorksWithoutErrors()
        {
            // Arrange 
            var passwords = new List<string>();
            var generator = new PasswordGenerator(seed: 0);

            // Act
            generator.Generate();

            // Assert 
        }


    }

}
