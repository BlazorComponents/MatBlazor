﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MatBlazor
{
    public class MatBlazorSwitchTDecimal : MatBlazorSwitchT<decimal>
    {
        public override decimal Clamp(decimal v, decimal min, decimal max)
        {
            return v < min ? min : v > max ? max : v;
        }

        public override decimal Increase(decimal v, decimal step)
        {
            checked
            {
                try
                {
                    return (decimal) (v + step);
                }
                catch (OverflowException)
                {
                    return decimal.MaxValue;
                }
            }
        }

        public override decimal Decrease(decimal v, decimal step)
        {
            checked
            {
                try
                {
                    return (decimal) (v - step);
                }
                catch (OverflowException)
                {
                    return decimal.MinValue;
                }
            }
        }

        public override decimal Round(decimal v, int dp)
        {
            return Math.Round(v, dp);
        }

        public override decimal GetMinimum() => decimal.MinValue;
        public override decimal GetMaximum() => decimal.MaxValue;

        public override decimal GetStep() => 1;

        public override string FormatValueAsString(decimal v, string format)
        {
            return v.ToString(format);
        }

        public override decimal ParseFromString(string v, string format)
        {
            return decimal.Parse(v, NumberStyles.Any);
        }

        public override decimal FromDateTimeNull(DateTime? v)
        {
            throw new NotImplementedException();
        }

        public override DateTime? ToDateTimeNull(decimal v)
        {
            throw new NotImplementedException();
        }

        public override decimal FromBoolNull(bool? v, bool indeterminate)
        {
            throw new NotImplementedException();
        }

        public override decimal FromDecimal(decimal v)
        {
            return v;
        }
    }
}