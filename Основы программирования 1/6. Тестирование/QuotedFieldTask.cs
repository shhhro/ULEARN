using System;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("'\"cat\"'", 0, "\"cat\"", 5)]
        [TestCase("'omg\\\' \\m'", 0, "omg' m", 7)]
        [TestCase("'xe'", 0, "xe", 4)]
        [TestCase("'omg' 'no'", 4, "no", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var newField = new StringBuilder();

            var currentIndex = startIndex;
            var flag = line[currentIndex++];

            while (currentIndex < line.Length)
            {
                var currentChar = line[currentIndex++];

                if (currentChar == flag)
                    return new Token(newField.ToString(), startIndex, currentIndex - startIndex);
                if (currentChar == '\\') 
                    currentChar = line[currentIndex++];

                newField.Append(currentChar);
            }
            return new Token(newField.ToString(), startIndex, currentIndex - startIndex);
        }
    }
}

/*
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }

        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            return new Token(line, startIndex, line.Length - startIndex);
        }
    }
}*/