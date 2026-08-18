using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C7C RID: 3196
	internal sealed class MaxDecimalAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x0600780A RID: 30730 RVA: 0x001BBD5D File Offset: 0x001B9F5D
		protected override object GetValueOverride()
		{
			return this.max;
		}

		// Token: 0x0600780B RID: 30731 RVA: 0x001BBD6A File Offset: 0x001B9F6A
		protected override void AccumulateOverride(object value)
		{
			this.max = Math.Max(this.max, Convert.ToDecimal(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x001BBD88 File Offset: 0x001B9F88
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			MaxDecimalAggregate maxDecimalAggregate = childAggregate as MaxDecimalAggregate;
			if (maxDecimalAggregate != null)
			{
				this.max = Math.Max(this.max, maxDecimalAggregate.max);
				return;
			}
			base.RaiseError();
		}

		// Token: 0x0600780D RID: 30733 RVA: 0x001BBDBD File Offset: 0x001B9FBD
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = (double)this.max;
			return true;
		}

		// Token: 0x040020DB RID: 8411
		private decimal max = decimal.MinValue;
	}
}
