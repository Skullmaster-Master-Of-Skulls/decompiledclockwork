using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C7D RID: 3197
	internal sealed class MaxIntAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x0600780F RID: 30735 RVA: 0x001BBDFF File Offset: 0x001B9FFF
		protected override object GetValueOverride()
		{
			return this.max;
		}

		// Token: 0x06007810 RID: 30736 RVA: 0x001BBE0C File Offset: 0x001BA00C
		protected override void AccumulateOverride(object value)
		{
			this.max = Math.Max(this.max, Convert.ToInt64(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06007811 RID: 30737 RVA: 0x001BBE2C File Offset: 0x001BA02C
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			MaxIntAggregate maxIntAggregate = childAggregate as MaxIntAggregate;
			if (maxIntAggregate != null)
			{
				this.max = Math.Max(this.max, maxIntAggregate.max);
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007812 RID: 30738 RVA: 0x001BBE61 File Offset: 0x001BA061
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

		// Token: 0x040020DC RID: 8412
		private long max = long.MinValue;
	}
}
