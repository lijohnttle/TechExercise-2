using TechExercise.Core.Analysis.Expressions;
using TechExercise.Core.Analysis.Expressions.Helpers;
using TechExercise.Core.Tokenization;

namespace TechExercise.Core.Analysis
{
    public class ExpressionBuilder
    {
        /// <summary>
        /// Builds an expression tree.
        /// </summary>
        public Expression BuildExpression(string expressionString, Token[] tokens)
        {
            Expression currentExpression = null;

            foreach (var token in tokens)
            {
                if (token.TokenType == TokenType.Variable)
                {
                    var variableName = expressionString.Substring(token.Position, token.Length);

                    currentExpression = currentExpression.ContinueWith(variableName);
                }
                else if (token.TokenType == TokenType.OperatorMinus && CheckIsUnary(currentExpression))
                {
                    currentExpression = currentExpression.ContinueWith(UnaryOperator.Minus);
                }
                else if (token.TokenType == TokenType.OpeningParentheses)
                {
                    currentExpression = currentExpression.OpenGroup();
                }
                else if (token.TokenType == TokenType.ClosingParentheses)
                {
                    currentExpression = currentExpression.CloseGroup();
                }
                else if (token.TokenType.TryConvertToBinaryOperator(out var binaryOperator))
                {
                    currentExpression = currentExpression.ContinueWith(binaryOperator);
                }
            }

            return currentExpression.GetRoot();
        }


        private static bool CheckIsUnary(Expression currentExpression)
        {
            if (currentExpression == null)
            {
                return true;
            }

            if (currentExpression is UnaryExpression unaryExpression && unaryExpression.Child == null)
            {
                return true;
            }

            return false;
        }
    }
}
