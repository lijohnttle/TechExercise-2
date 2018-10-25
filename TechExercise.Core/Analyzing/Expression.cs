using System.Collections.Generic;

namespace TechExercise.Core.Analyzing
{
    public abstract class Expression
    {
        public Expression Parent { get; set; }


        public abstract double Calculate(IDictionary<string, int> variableSet);
    }
}
