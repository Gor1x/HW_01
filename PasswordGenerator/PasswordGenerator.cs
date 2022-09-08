using System;
using System.Collections.Generic;
using System.Linq;

namespace HW1.PasswordGenerator
{

    public class PasswordGenerator
    {
        private readonly Random _rand;

        private static readonly List<char> NumberSymbols =
            Enumerable.Range(0, 10).Select(number => (char)(number + '0')).ToList();
        private static readonly List<char> LetterSymbolsLower =
            Enumerable.Range(0, 26).Select(i => (char)('a' + i)).ToList();
        private static readonly List<char> LetterSymbolsUpper =
            Enumerable.Range(0, 26).Select(i => (char)('A' + i)).ToList();

        public PasswordGenerator()
        {
            _rand = new Random();
        }
        public PasswordGenerator(int seed)
        {
            _rand = new Random(seed);
        }

        public string Generate()
        {
            var passwordLength = _rand.Next(3, 17);
            var password = RecursiveGenerator(passwordLength, false, 5);
            return new List<char>
            {
                RandomElementFrom(LetterSymbolsUpper),
                RandomElementFrom(LetterSymbolsUpper),
                '_'
            }.Aggregate(password, WithRandomlyAddedSymbol);
        }

        private char RandomElementFrom(IReadOnlyList<char> data) => data[_rand.Next() % data.Count];
        private string WithRandomlyAddedSymbol(string str, char symbol)
        {
            var index = _rand.Next() % str.Length;
            return str.Insert(index, symbol.ToString());
        }
        private string RecursiveGenerator(int length, bool isPreviousNumber, int maxNumberCount)
        {
            if (length == 0)
                return "";

            var elements = new List<char>();
            elements.AddRange(LetterSymbolsLower);
            elements.AddRange(LetterSymbolsUpper);
            if (!isPreviousNumber && maxNumberCount > 0)
            {
                elements.AddRange(NumberSymbols);
                var symbol = RandomElementFrom(elements);
                var isNumber = '0' <= symbol && symbol <= '9';
                if (isNumber)
                    maxNumberCount -= 1;
                return symbol + RecursiveGenerator(length - 1, isNumber, maxNumberCount);
            }
            else
            {
                var symbol = RandomElementFrom(elements);
                return symbol + RecursiveGenerator(length - 1, false, maxNumberCount);
            }
        }
    }
}
