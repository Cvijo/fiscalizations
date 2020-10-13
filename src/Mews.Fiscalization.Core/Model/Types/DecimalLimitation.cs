using System;

namespace Mews.Fiscalization.Core.Model
{
    public class DecimalLimitation : ILimitation<decimal>
    {
        public DecimalLimitation(decimal? min = null, decimal? max = null, int? maxDecimalPlaces = null, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            Range = new RangeLimitation<decimal>(min: min, max: max, minIsAllowed: minIsAllowed, maxIsAllowed: maxIsAllowed);
            MaxDecimalPlaces = maxDecimalPlaces;
        }

        private RangeLimitation<decimal> Range { get; }

        private int? MaxDecimalPlaces { get; }

        public bool IsValid(decimal value)
        {
            return Range.IsValid(value) && PrecisionIsValid(value);
        }

        public void CheckValidity(decimal value)
        {
            Range.CheckValidity(value, label: "value");
            if (!PrecisionIsValid(value))
            {
                throw new ArgumentException($"Highest possible precision is {MaxDecimalPlaces} decimal places.");
            }
        }

        private bool PrecisionIsValid(decimal value)
        {
            if (MaxDecimalPlaces.HasValue)
            {
                var divisor = (decimal)Math.Pow(0.1, MaxDecimalPlaces.Value);
                return value % divisor == 0;
            }

            return true;
        }
    }
}