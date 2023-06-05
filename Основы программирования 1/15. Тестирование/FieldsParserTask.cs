using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("cat", new[] { "cat" })]
        [TestCase("little cat", new[] { "little", "cat" })]
        [TestCase("cute 'cat'", new[] { "cute", "cat" })]
        [TestCase(@"'wow\\'", new[] { @"wow\" })]
        [TestCase(@"'\'nice'", new[] { @"'nice" })]
        [TestCase("", new string[0])]
        [TestCase("\"\'friday\'\" paper", new[] { "'friday'", "paper" })]
        [TestCase("\'\"zelda\"\' game", new[] { "\"zelda\"", "game" })]
        [TestCase("\'\"\'", new[] { "\"" })]
        [TestCase("\"\"", new[] { "" })]
        [TestCase("black \"cat", new[] { "black", "cat" })]
        [TestCase("\'sleep ", new[] { "sleep " })]
        [TestCase(@"'cat''dog'", new[] { "cat", "dog" })]
        [TestCase(@"""\""field""", new[] { @"""field" })]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var listOfTokens = new List<Token>();
            var lengthOfLine = line.Length;

            for (var element = 0; element < lengthOfLine; element++)
            {
                Token token;

                if (line[element] == '\'' || line[element] == '\"')
                    token = ReadQuotedField(line, element);
                else 
                    token = ReadField(line, element);
                if (token.Length > 1) 
                    element += token.Length - 1;
                listOfTokens.Add(token);
            }
            return CreateFinalList(listOfTokens);
        }
        private static List<Token> CreateFinalList(List<Token> listOfTokens)
        {
            var resultToken = new List<Token>();
            foreach (var token in listOfTokens)
                if (token.Length > 0) resultToken.Add(token);
            return resultToken;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var newWord = new StringBuilder();
            var lengthOfText = line.Length;

            for (var index = startIndex; index < lengthOfText; index++)
            {
                if (line[index] == '\'' || line[index] == '\"' || line[index] == ' ')
                    return new Token(newWord.ToString(), startIndex, newWord.Length);
                newWord.Append(line[index]);
            }
            return new Token(newWord.ToString(), startIndex, newWord.Length);
        }

        private static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}