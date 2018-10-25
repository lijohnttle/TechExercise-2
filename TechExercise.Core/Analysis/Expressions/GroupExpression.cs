using System.Collections.Generic;

namespace TechExercise.Core.Analysis.Expressions
{
    public class GroupExpression : Expression, IExpressionContainer
    {
        private Expression _child;


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

        public bool IsClosed { get; set; }


        public override double Calculate(IDictionary<string, int> variableSet) => Child.Calculate(variableSet);

        public override bool Equals(object obj)
        {
            if (obj is GroupExpression other)
            {
                return Equals(other);
            }

            return false;
        }

        protected bool Equals(GroupExpression other)
        {
            if (IsClosed != other.IsClosed)
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
