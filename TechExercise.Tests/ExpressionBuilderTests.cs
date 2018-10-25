using System.Collections.Generic;
using TechExercise.Core.Analysis;
using TechExercise.Core.Analysis.Expressions;
using TechExercise.Core.Tokenization;
using Xunit;

namespace TechExercise.Tests
{
    public class ExpressionBuilderTests
    {
        [Theory]
        [MemberData(nameof(GetBuildExpressionTestData))]
        public void BuildExpression(string expressionString, Expression expectedExpression)
        {
            // Arrange
            var parser = new TokenBuilder();
            var analyzer = new ExpressionBuilder();

            // Act
            var tokens = parser.Build(expressionString);
            var expression = analyzer.BuildExpression(expressionString, tokens);

            // Assert
            Assert.Equal(expectedExpression, expression);
        }


        public static IEnumerable<object[]> GetBuildExpressionTestData()
        {
            yield return new object[]
            {
                "a",
                new VariableExpression("a")
            };
            yield return new object[]
            {
                "-a",
                new UnaryExpression(UnaryOperator.Minus)
                {
                    Child = new VariableExpression("a")
                }
            };
            yield return new object[]
            {
                "a-b",
                new BinaryExpression(BinaryOperator.Minus)
                {
                    LeftChild = new VariableExpression("a"),
                    RightChild = new VariableExpression("b")
                }
            };
            yield return new object[]
            {
                "a-b+c",
                new BinaryExpression(BinaryOperator.Plus)
                {
                    LeftChild = new BinaryExpression(BinaryOperator.Minus)
                    {
                        LeftChild = new VariableExpression("a"),
                        RightChild = new VariableExpression("b")
                    },
                    RightChild = new VariableExpression("c")
                }
            };
            yield return new object[]
            {
                "a*b+c",
                new BinaryExpression(BinaryOperator.Plus)
                {
                    LeftChild = new BinaryExpression(BinaryOperator.Multiply)
                    {
                        LeftChild = new VariableExpression("a"),
                        RightChild = new VariableExpression("b")
                    },
                    RightChild = new VariableExpression("c")
                }
            };
            yield return new object[]
            {
                "a-b+c*d",
                new BinaryExpression(BinaryOperator.Plus)
                {
                    LeftChild = new BinaryExpression(BinaryOperator.Minus)
                    {
                        LeftChild = new VariableExpression("a"),
                        RightChild = new VariableExpression("b")
                    },
                    RightChild = new BinaryExpression(BinaryOperator.Multiply)
                    {
                        LeftChild = new VariableExpression("c"),
                        RightChild = new VariableExpression("d")
                    }
                }
            };
            yield return new object[]
            {
                "a-b+c*d^e",
                new BinaryExpression(BinaryOperator.Plus)
                {
                    LeftChild = new BinaryExpression(BinaryOperator.Minus)
                    {
                        LeftChild = new VariableExpression("a"),
                        RightChild = new VariableExpression("b")
                    },
                    RightChild = new BinaryExpression(BinaryOperator.Multiply)
                    {
                        LeftChild = new VariableExpression("c"),
                        RightChild = new BinaryExpression(BinaryOperator.Power)
                        {
                            LeftChild = new VariableExpression("d"),
                            RightChild = new VariableExpression("e")
                        }
                    }
                }
            };
            yield return new object[]
            {
                "(a+b)*(c-d)",
                new BinaryExpression(BinaryOperator.Multiply)
                {
                    LeftChild = new GroupExpression
                    {
                        IsClosed = true,
                        Child = new BinaryExpression(BinaryOperator.Plus)
                        {
                            LeftChild = new VariableExpression("a"),
                            RightChild = new VariableExpression("b")
                        }
                    },
                    RightChild = new GroupExpression
                    {
                        IsClosed = true,
                        Child = new BinaryExpression(BinaryOperator.Minus)
                        {
                            LeftChild = new VariableExpression("c"),
                            RightChild = new VariableExpression("d")
                        }
                    }
                }
            };
            yield return new object[]
            {
                "(a+b)*(c-d)^e",
                new BinaryExpression(BinaryOperator.Multiply)
                {
                    LeftChild = new GroupExpression
                    {
                        IsClosed = true,
                        Child = new BinaryExpression(BinaryOperator.Plus)
                        {
                            LeftChild = new VariableExpression("a"),
                            RightChild = new VariableExpression("b")
                        }
                    },
                    RightChild = new BinaryExpression(BinaryOperator.Power)
                    {
                        LeftChild = new GroupExpression
                        {
                            IsClosed = true,
                            Child = new BinaryExpression(BinaryOperator.Minus)
                            {
                                LeftChild = new VariableExpression("c"),
                                RightChild = new VariableExpression("d")
                            }
                        },
                        RightChild = new VariableExpression("e")
                    }
                }
            };
            yield return new object[]
            {
                "((a + b))",
                new GroupExpression
                {
                    IsClosed = true,
                    Child = new GroupExpression
                    {
                        IsClosed = true,
                        Child = new BinaryExpression(BinaryOperator.Plus)
                        {
                            LeftChild = new VariableExpression("a"),
                            RightChild = new VariableExpression("b")
                        }
                    }
                }
            };
        }
    }
}
