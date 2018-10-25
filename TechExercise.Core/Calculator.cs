using System;
using System.Collections.Generic;
using System.Linq;
using TechExercise.Core.Analyzing;
using TechExercise.Core.Analyzing.Expressions.Helpers;
using TechExercise.Core.Tokenization;

namespace TechExercise.Core
{
    public class Calculator
    {
        private readonly TokenBuilder _tokenBuilder;
        private readonly ExpressionBuilder _expressionBuilder;


        public Calculator()
        {
            _tokenBuilder = new TokenBuilder();
            _expressionBuilder = new ExpressionBuilder();
        }


        public CalculateResult Calculate(string expressionString)
        {
            var tokens = _tokenBuilder.Build(expressionString);
            var expression = _expressionBuilder.BuildExpression(expressionString, tokens);

            return CalculateFromExpression(expression);
        }



        private static CalculateResult CalculateFromExpression(Expression expression)
        {
            var variableNames = expression.GetVariableNames().ToArray();
            var variationCount = (long)Math.Pow(2, variableNames.Length);

            var bestResult = (double?)null;
            var bestVariableSet = (Dictionary<string, int>)null;

            for (long variation = 0; variation < variationCount; variation++)
            {
                var variableSet = GenerateVariableSet(variation, variableNames);

                var result = expression.Calculate(variableSet);

                if (!double.IsNaN(result) && !double.IsInfinity(result))
                {
                    if (bestResult == null || bestResult < result)
                    {
                        bestResult = result;
                        bestVariableSet = variableSet;
                    }
                }
            }

            return new CalculateResult(bestResult ?? 0, bestVariableSet);
        }

        private static Dictionary<string, int> GenerateVariableSet(long variation, IReadOnlyList<string> variableNames)
        {
            var variableSet = new Dictionary<string, int>();

            for (var variableIndex = 0; variableIndex < variableNames.Count; variableIndex++)
            {
                variableSet[variableNames[variableIndex]] = (int)((variation >> variableIndex) % 2);
            }

            return variableSet;
        }
    }
}
