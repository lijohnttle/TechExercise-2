using System;

namespace TechExercise.Core.Tokenization
{
    public class TokenizeRule
    {
        private readonly Func<char, bool> _selector;
        private readonly Func<TokenType?, bool> _canMerge;


        public TokenizeRule(char character, TokenType tokenType)
            : this(c => c == character, tokenType)
        {
            
        }

        public TokenizeRule(char character, TokenType tokenType, Func<TokenType?, bool> canMerge)
            : this(c => c == character, tokenType, canMerge)
        {

        }

        public TokenizeRule(Func<char, bool> selector, TokenType tokenType)
            : this(selector, tokenType, tt => false)
        {
            
        }

        public TokenizeRule(Func<char, bool> selector, TokenType tokenType, Func<TokenType?, bool> canMerge)
        {
            TokenType = tokenType;
            _selector = selector;
            _canMerge = canMerge;
        }


        public TokenType TokenType { get; }


        public bool Select(char character) => _selector(character);

        public bool CanMerge(TokenType? prevTokenType) => _canMerge(prevTokenType);
    }
}
