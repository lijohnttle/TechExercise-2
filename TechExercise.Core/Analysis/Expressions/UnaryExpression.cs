using System.Collections.Generic;

namespace TechExercise.Core.Analysis.Expressions
{
    public enum UnaryOperator
    {
        Minus
    }

    public class UnaryExpression : Expression, IExpressionContainer
    {
        private Expression _child;


        public UnaryExpression(UnaryOperator @operator)
        {
            Operator = @operator;
        }


        public UnaryOperator Operator { get; }

        public Expression Child
        {
            get => _child;
            set
            {
                _child = value;

                if (_child != null)
                {
                    _child.Parent = this;
                }
            }
        }


        public override double Calculate(IDictionary<string, int> variableSet)
        {
            var result = Child.Calculate(variableSet);

            if (Operator == UnaryOperator.Minus)
            {
                result = -result;
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is UnaryExpression other)
            {
                return Equals(other);
            }

            return false;
        }

        protected bool Equals(UnaryExpression other)
        {
            if (Operator != other.Operator)
            {
                return false;
            }

            if (Child == null)
            {
                return other.Child == null;
            }

            return Child.Equals(other.Child);
        }

        public override int GetHashCode() => 0;

        public IEnumerable<Expression> GetChildren()
        {
            yield return Child;
        }
    }
}
