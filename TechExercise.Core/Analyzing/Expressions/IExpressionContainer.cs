using System.Collections.Generic;

namespace TechExercise.Core.Analyzing.Expressions
{
    public interface IExpressionContainer
    {
        IEnumerable<Expression> GetChildren();
    }
}
