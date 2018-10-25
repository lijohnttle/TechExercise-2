using System.Collections.Generic;
using TechExercise.Core;
using Xunit;

namespace TechExercise.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [MemberData(nameof(GetCalculateTestData))]
        public void Calculate(string expression, CalculateResult expectedResult)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(expression);

            // Assert
            Assert.Equal(expectedResult.Result, result.Result);
            Assert.Equal(expectedResult, result);
        }


        public static IEnumerable<object[]> GetCalculateTestData()
        {
            yield return new object[]
            {
                "a", 
                new CalculateResult(1, new Dictionary<string, int>
                {
                    {"a", 1}
                })
            };
            yield return new object[]
            {
                "a+b+c+d^e",
                new CalculateResult(4, new Dictionary<string, int>
                {
                    {"a", 1},
                    {"b", 1},
                    {"c", 1},
                    {"d", 0},
                    {"e", 0},
                })
            };
            yield return new object[]
            {
                "((a+b)*(b^c+(d-e)/a))^(c+d/e)",
                new CalculateResult(4, new Dictionary<string, int>
                {
                    {"a", 1},
                    {"b", 1},
                    {"c", 1},
                    {"d", 1},
                    {"e", 1},
                })
            };
        }
    }
}
