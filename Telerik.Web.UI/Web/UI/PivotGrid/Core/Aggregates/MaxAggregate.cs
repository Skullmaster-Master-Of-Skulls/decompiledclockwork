using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C7B RID: 3195
	internal sealed class MaxAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007805 RID: 30725 RVA: 0x001BBCA8 File Offset: 0x001B9EA8
		protected override object GetValueOverride()
		{
			return this.max;
		}

		// Token: 0x06007806 RID: 30726 RVA: 0x001BBCB5 File Offset: 0x001B9EB5
		protected override void AccumulateOverride(object value)
		{
			this.max = Math.Max(this.max, Convert.ToDouble(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06007807 RID: 30727 RVA: 0x001BBCD4 File Offset: 0x001B9ED4
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			MaxAggregate maxAggregate = childAggregate as MaxAggregate;
			if (maxAggregate != null)
			{
				this.max = Math.Max(this.max, maxAggregate.max);
				return;
			}
			double val;
			if (childAggregate.TryConvertValue(out val))
			{
				this.max = Math.Max(this.max, val);
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007808 RID: 30728 RVA: 0x001BBD26 File Offset: 0x001B9F26
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.max;
			return true;
		}

		// Token: 0x040020DA RID: 8410
		private double max = double.NegativeInfinity;
	}
}
