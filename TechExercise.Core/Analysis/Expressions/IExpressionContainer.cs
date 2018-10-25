using System.Collections.Generic;

namespace TechExercise.Core.Analysis.Expressions
{
    public interface IExpressionContainer
    {
        IEnumerable<Expression> GetChildren();
    }
}
