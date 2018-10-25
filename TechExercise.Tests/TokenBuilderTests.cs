using System.Collections.Generic;
using System.Linq;
using TechExercise.Core.Tokenization;
using Xunit;

namespace TechExercise.Tests
{
    public class TokenBuilderTests
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Build(string expression, Token[] expectedTokens)
        {
            // Arrange
            var parser = new TokenBuilder();

            // Act
            var tokens = parser.Build(expression);

            // Assert
            Assert.True(CompareTokenSets(expectedTokens, tokens));
        }


        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                "a",
                new []
                {
                    new Token(0, 1, TokenType.Variable)
                }
            };
            yield return new object[]
            {
                "-a",
                new []
                {
                    new Token(0, 1, TokenType.OperatorMinus),
                    new Token(1, 1, TokenType.Variable)
                }
            };
            yield return new object[]
            {
                "((a+b)*(b^c+(d-e)/a))^(c+d/e)",
                new []
                {
                    new Token(0, 1, TokenType.OpeningParentheses),
                    new Token(1, 1, TokenType.OpeningParentheses),
                    new Token(2, 1, TokenType.Variable),
                    new Token(3, 1, TokenType.OperatorPlus),
                    new Token(4, 1, TokenType.Variable),
                    new Token(5, 1, TokenType.ClosingParentheses),
                    new Token(6, 1, TokenType.OperatorMultiply),
                    new Token(7, 1, TokenType.OpeningParentheses),
                    new Token(8, 1, TokenType.Variable),
                    new Token(9, 1, TokenType.OperatorPower),
                    new Token(10, 1, TokenType.Variable),
                    new Token(11, 1, TokenType.OperatorPlus),
                    new Token(12, 1, TokenType.OpeningParentheses),
                    new Token(13, 1, TokenType.Variable),
                    new Token(14, 1, TokenType.OperatorMinus),
                    new Token(15, 1, TokenType.Variable),
                    new Token(16, 1, TokenType.ClosingParentheses),
                    new Token(17, 1, TokenType.OperatorDivide),
                    new Token(18, 1, TokenType.Variable),
                    new Token(19, 1, TokenType.ClosingParentheses),
                    new Token(20, 1, TokenType.ClosingParentheses),
                    new Token(21, 1, TokenType.OperatorPower),
                    new Token(22, 1, TokenType.OpeningParentheses),
                    new Token(23, 1, TokenType.Variable),
                    new Token(24, 1, TokenType.OperatorPlus),
                    new Token(25, 1, TokenType.Variable),
                    new Token(26, 1, TokenType.OperatorDivide),
                    new Token(27, 1, TokenType.Variable),
                    new Token(28, 1, TokenType.ClosingParentheses),
                }
            };
        }

        private static bool CompareTokenSets(IEnumerable<Token> tokens1, IEnumerable<Token> tokens2)
        {
            var tokens1List = new List<Token>(tokens1 ?? Enumerable.Empty<Token>());
            var tokens2List = new List<Token>(tokens2 ?? Enumerable.Empty<Token>());

            if (tokens1List.Count != tokens2List.Count)
            {
                return false;
            }

            for (var i = 0; i < tokens1List.Count; i++)
            {
                var token1 = tokens1List[i];
                var token2 = tokens2List[i];

                if (!token1.Equals(token2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
