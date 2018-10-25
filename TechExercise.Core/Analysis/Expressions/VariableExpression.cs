using System;
using System.Collections.Generic;

namespace TechExercise.Core.Analysis.Expressions
{
    public class VariableExpression : Expression
    {
        public VariableExpression(string variableName)
        {
            VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
        }


        public string VariableName { get; }


        public override double Calculate(IDictionary<string, int> variableSet) => variableSet[VariableName];

        public override bool Equals(object obj)
        {
            if (obj is VariableExpression other)
            {
                return Equals(other);
            }

            return false;
        }

        protected bool Equals(VariableExpression other)
        {
            return string.Equals(VariableName, other.VariableName);
        }

        public override int GetHashCode() => 0;
    }
}
