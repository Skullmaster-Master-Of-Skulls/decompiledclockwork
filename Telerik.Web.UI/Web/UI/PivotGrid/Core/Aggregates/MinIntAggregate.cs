using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C80 RID: 3200
	internal sealed class MinIntAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x0600781E RID: 30750 RVA: 0x001BBFEB File Offset: 0x001BA1EB
		protected override object GetValueOverride()
		{
			return this.min;
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x001BBFF8 File Offset: 0x001BA1F8
		protected override void AccumulateOverride(object value)
		{
			this.min = Math.Min(this.min, Convert.ToInt64(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x001BC018 File Offset: 0x001BA218
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			MinIntAggregate minIntAggregate = childAggregate as MinIntAggregate;
			if (minIntAggregate != null)
			{
				this.min = Math.Min(this.min, minIntAggregate.min);
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007821 RID: 30753 RVA: 0x001BC04D File Offset: 0x001BA24D
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = (double)this.min;
			return true;
		}

		// Token: 0x040020DF RID: 8415
		private long min = long.MaxValue;
	}
}
