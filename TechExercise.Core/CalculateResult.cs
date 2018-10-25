using System;
using System.Collections.Generic;

namespace TechExercise.Core
{
    public class CalculateResult : IEquatable<CalculateResult>
    {
        public CalculateResult(double result, Dictionary<string, int> variables)
        {
            Variables = variables ?? throw new ArgumentNullException(nameof(variables));
            Result = result;
        }


        public Dictionary<string, int> Variables { get; }

        public double Result { get; }


        public bool Equals(CalculateResult other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!Result.Equals(other.Result))
            {
                return false;
            }

            if (Variables.Count != other.Variables.Count)
            {
                return false;
            }

            foreach (var varEntry in Variables)
            {
                if (!other.Variables.TryGetValue(varEntry.Key, out var varValue) ||
                    varValue != varEntry.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is CalculateResult other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Variables.GetHashCode() * 397) ^ Result.GetHashCode();
            }
        }
    }
}
