using System;

namespace TechExercise.Core.Tokenization
{
    public class Token : IEquatable<Token>
    {
        public Token(int position, int length, TokenType tokenType)
        {
            Position = position;
            Length = length;
            TokenType = tokenType;
        }


        public int Position { get; }

        public int Length { get; }

        public TokenType TokenType { get; }


        public bool Equals(Token other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Position == other.Position && Length == other.Length && TokenType == other.TokenType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position;
                hashCode = (hashCode * 397) ^ Length;
                hashCode = (hashCode * 397) ^ (int) TokenType;
                return hashCode;
            }
        }
    }
}
