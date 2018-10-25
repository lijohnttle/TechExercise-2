# TechExercise-2

Given a mathematical expression, e.g. ((a+b)*(b^c+(d-e)/a))^(c+d/e). The number of variables can be unlimited. However, variables can only be either 1 or 0. The application finds the value of each variable, which results in the expression having the maximum possible value.

## Projects

#### TechExercise.Core
.NET Core library with a calculator class - **Calculator**.

#### TechExercise.Tests
.NET Core unit tests library (XUnit).

## Example

Code example:

```
var expression = "((a+b)*(b^c+(d-e)/a))^(c+d/e)";
var calculator = new Calculator();
var calculationResult = calculator.Calculate(expression);

Console.WriteLine($"Result = {calculationResult.Result}");
Console.WriteLine($"Variable values:");

foreach (var variableEntry in calculationResult.Variables)
{
    Console.WriteLine($"{variableEntry.Key} = {variableEntry.Value}");
}
```

Output:

```
Result = 4
Variable values:
a = 1
b = 1
c = 1
d = 1
e = 1
```
