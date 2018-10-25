using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechExercise.Core.Tokenization
{
    public class TokenBuilder
    {
        private readonly TokenizeRule[] _rules;


        public TokenBuilder()
        {
            _rules = new[]
            {
                new TokenizeRule('(', TokenType.OpeningParentheses),
                new TokenizeRule(')', TokenType.ClosingParentheses),
                new TokenizeRule('+', TokenType.OperatorPlus),
                new TokenizeRule('-', TokenType.OperatorMinus),
                new TokenizeRule('*', TokenType.OperatorMultiply),
                new TokenizeRule('/', TokenType.OperatorDivide),
                new TokenizeRule('^', TokenType.OperatorPower),
                new TokenizeRule(char.IsLetter, TokenType.Variable,
                    tt => tt == TokenType.Variable),
                new TokenizeRule(char.IsWhiteSpace, TokenType.Whitespace,
                    tt => tt == TokenType.Whitespace),
            };
        }


        public Token[] Build(string expression)
        {
            var tokens = new List<Token>();
            var tokenBuilder = new StringBuilder();
            var tokenType = (TokenType?) null;
            var tokenPosition = 0;
            var charPosition = 0;
            char character;

            for (; charPosition < expression.Length; charPosition++)
            {
                character = expression[charPosition];

                var rule = _rules.FirstOrDefault(t => t.Select(character));
                if (rule != null)
                {
                    if (!rule.CanMerge(tokenType))
                    {
                        BreakToken();
                    }

                    tokenType = rule.TokenType;
                    tokenBuilder.Append(character);
                }
            }

            BreakToken();

            return tokens.ToArray();


            void BreakToken()
            {
                if (tokenType != null)
                {
                    tokens.Add(new Token(tokenPosition, tokenBuilder.Length, tokenType.Value));

                    tokenBuilder.Clear();
                    tokenType = null;
                }

                tokenPosition = charPosition;
            }
        }
    }
}
