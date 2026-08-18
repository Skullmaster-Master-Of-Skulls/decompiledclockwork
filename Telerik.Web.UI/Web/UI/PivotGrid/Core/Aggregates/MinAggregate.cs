using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C7E RID: 3198
	internal sealed class MinAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007814 RID: 30740 RVA: 0x001BBE99 File Offset: 0x001BA099
		protected override object GetValueOverride()
		{
			return this.min;
		}

		// Token: 0x06007815 RID: 30741 RVA: 0x001BBEA6 File Offset: 0x001BA0A6
		protected override void AccumulateOverride(object value)
		{
			this.min = Math.Min(this.min, Convert.ToDouble(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06007816 RID: 30742 RVA: 0x001BBEC4 File Offset: 0x001BA0C4
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			MinAggregate minAggregate = childAggregate as MinAggregate;
			if (minAggregate != null)
			{
				this.min = Math.Min(this.min, minAggregate.min);
				return;
			}
			double val;
			if (childAggregate.TryConvertValue(out val))
			{
				this.min = Math.Min(this.min, val);
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007817 RID: 30743 RVA: 0x001BBF16 File Offset: 0x001BA116
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.min;
			return true;
		}

		// Token: 0x040020DD RID: 8413
		private double min = double.PositiveInfinity;
	}
}
