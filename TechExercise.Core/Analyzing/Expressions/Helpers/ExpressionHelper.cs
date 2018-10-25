using System;
using System.Collections.Generic;
using TechExercise.Core.Tokenization;

namespace TechExercise.Core.Analyzing.Expressions.Helpers
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Continue expression tree with variable expresssion.
        /// </summary>
        public static Expression ContinueWith(this Expression expression, string variableName)
        {
            if (expression == null)
            {
                return new VariableExpression(variableName);
            }

            if (expression is GroupExpression groupExpression)
            {
                if (groupExpression.Child == null)
                {
                    return groupExpression.Child = new VariableExpression(variableName);
                }
            }

            if (expression is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Child == null)
                {
                    return unaryExpression.Child = new VariableExpression(variableName);
                }
            }

            if (expression is BinaryExpression binaryExpression)
            {
                if (binaryExpression.RightChild == null)
                {
                    return binaryExpression.RightChild = new VariableExpression(variableName);
                }
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Continue expression tree with unary expresssion.
        /// </summary>
        public static Expression ContinueWith(this Expression expression, UnaryOperator unaryOperator)
        {
            if (expression == null)
            {
                return new UnaryExpression(unaryOperator);
            }

            if (expression is GroupExpression groupExpression)
            {
                if (groupExpression.Child == null)
                {
                    return groupExpression.Child = new UnaryExpression(unaryOperator);
                }
            }

            if (expression is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Child == null)
                {
                    return unaryExpression.Child = new UnaryExpression(unaryOperator);
                }
            }

            if (expression is BinaryExpression binaryExpression)
            {
                if (binaryExpression.RightChild == null)
                {
                    return binaryExpression.RightChild = new UnaryExpression(unaryOperator);
                }
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Continue expression tree with binary expresssion.
        /// </summary>
        public static Expression ContinueWith(this Expression expression, BinaryOperator binaryOperator)
        {
            if (expression == null)
            {
                return new BinaryExpression(binaryOperator);
            }

            if (expression is GroupExpression groupExpression && !groupExpression.IsClosed)
            {
                var child = groupExpression.Child;

                return groupExpression.Child = new BinaryExpression(binaryOperator)
                {
                    LeftChild = child
                };
            }

            if (expression is BinaryExpression binaryExpression)
            {
                if (binaryOperator.GetOperatorPriority() > binaryExpression.Operator.GetOperatorPriority())
                {
                    var rightChild = binaryExpression.RightChild;

                    return binaryExpression.RightChild = new BinaryExpression(binaryOperator)
                    {
                        LeftChild = rightChild
                    };
                }
            }

            if (expression.Parent == null)
            {
                return new BinaryExpression(binaryOperator)
                {
                    LeftChild = expression
                };
            }

            return expression.Parent.ContinueWith(binaryOperator);
        }

        public static Expression OpenGroup(this Expression expression)
        {
            if (expression == null)
            {
                return new GroupExpression();
            }

            if (expression is UnaryExpression unaryExpression)
            {
                return unaryExpression.Child = new GroupExpression();
            }

            if (expression is GroupExpression groupExpression)
            {
                return groupExpression.Child = new GroupExpression();
            }

            if (expression is BinaryExpression binaryExpression)
            {
                if (binaryExpression.RightChild == null)
                {
                    return binaryExpression.RightChild = new GroupExpression();
                }
            }

            return expression.Parent.CloseGroup();
        }

        public static Expression CloseGroup(this Expression expression)
        {
            while (expression != null)
            {
                if (expression is GroupExpression groupExpression && !groupExpression.IsClosed)
                {
                    groupExpression.IsClosed = true;

                    return groupExpression;
                }

                expression = expression.Parent;
            }

            return null;
        }

        public static bool TryConvertToBinaryOperator(this TokenType tokenType, out BinaryOperator binaryOperator)
        {
            switch (tokenType)
            {
                case TokenType.OperatorPlus:
                    binaryOperator = BinaryOperator.Plus;
                    return true;
                case TokenType.OperatorMinus:
                    binaryOperator = BinaryOperator.Minus;
                    return true;
                case TokenType.OperatorMultiply:
                    binaryOperator = BinaryOperator.Multiply;
                    return true;
                case TokenType.OperatorDivide:
                    binaryOperator = BinaryOperator.Divide;
                    return true;
                case TokenType.OperatorPower:
                    binaryOperator = BinaryOperator.Power;
                    return true;
                default:
                    binaryOperator = default(BinaryOperator);
                    return false;
            }
        }

        public static int GetOperatorPriority(this BinaryOperator binaryOperator)
        {
            switch (binaryOperator)
            {
                case BinaryOperator.Multiply:
                case BinaryOperator.Divide:
                    return 1;
                case BinaryOperator.Power:
                    return 2;
                default:
                    return 0;
            }
        }

        public static Expression GetRoot(this Expression expression)
        {
            if (expression == null)
            {
                return null;
            }

            var currentExpression = expression;

            while (currentExpression.Parent != null)
            {
                currentExpression = currentExpression.Parent;
            }

            return currentExpression;
        }

        public static ISet<string> GetVariableNames(this Expression expression)
        {
            var result = new HashSet<string>();

            var expressions = new Stack<Expression>();
            expressions.Push(expression);

            while (expressions.Count > 0)
            {
                var currentExpression = expressions.Pop();

                if (currentExpression is VariableExpression variableExpression)
                {
                    result.Add(variableExpression.VariableName);
                    continue;
                }

                if (currentExpression is IExpressionContainer expressionContainer)
                {
                    foreach (var child in expressionContainer.GetChildren())
                    {
                        expressions.Push(child);
                    }

                    continue;
                }
            }

            return result;
        }
    }
}
