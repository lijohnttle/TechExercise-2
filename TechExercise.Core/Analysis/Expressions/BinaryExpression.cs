using System;
using System.Collections.Generic;

namespace TechExercise.Core.Analysis.Expressions
{
    public enum BinaryOperator
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Power
    }

    public class BinaryExpression : Expression, IExpressionContainer
    {
        private Expression _rightChild;
        private Expression _leftChild;


        public BinaryExpression(BinaryOperator @operator)
        {
            Operator = @operator;
        }


        public BinaryOperator Operator { get; set; }

        public Expression LeftChild
        {
            get => _leftChild;
            set
            {
                _leftChild = value;

                if (_leftChild != null)
                {
                    _leftChild.Parent = this;
                }
            }
        }

        public Expression RightChild
        {
            get => _rightChild;
            set
            {
                _rightChild = value;

                if (_rightChild != null)
                {
                    _rightChild.Parent = this;
                }
            }
        }


        public override double Calculate(IDictionary<string, int> variableSet)
        {
            var leftValue = LeftChild.Calculate(variableSet);
            var rightValue = RightChild.Calculate(variableSet);

            switch (Operator)
            {
                case BinaryOperator.Plus:
                    return leftValue + rightValue;

                case BinaryOperator.Minus:
                    return leftValue - rightValue;

                case BinaryOperator.Multiply:
                    return leftValue * rightValue;

                case BinaryOperator.Divide:
                    return leftValue / rightValue;

                case BinaryOperator.Power:
                    return Math.Pow(leftValue, rightValue);
            }

            throw new ArgumentOutOfRangeException();
        }

        public override bool Equals(object obj)
        {
            if (obj is BinaryExpression other)
            {
                return Equals(other);
            }

            return false;
        }

        protected bool Equals(BinaryExpression other)
        {
            if (Operator != other.Operator)
            {
                return false;
            }

            if (LeftChild == null)
            {
                if (other.LeftChild != null)
                {
                    return false;
                }
            }
            else if (!LeftChild.Equals(other.LeftChild))
            {
                return false;
            }

            if (RightChild == null)
            {
                if (other.RightChild != null)
                {
                    return false;
                }
            }
            else if (!RightChild.Equals(other.RightChild))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode() => 0;

        public IEnumerable<Expression> GetChildren()
        {
            yield return LeftChild;
            yield return RightChild;
        }
    }
}
