using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C87 RID: 3207
	internal sealed class SumIntAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007841 RID: 30785 RVA: 0x001BC497 File Offset: 0x001BA697
		protected override object GetValueOverride()
		{
			return this.sum;
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x001BC4A4 File Offset: 0x001BA6A4
		protected override void AccumulateOverride(object value)
		{
			this.sum += Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06007843 RID: 30787 RVA: 0x001BC4C0 File Offset: 0x001BA6C0
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			SumIntAggregate sumIntAggregate = childAggregate as SumIntAggregate;
			if (sumIntAggregate != null)
			{
				this.sum += sumIntAggregate.sum;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007844 RID: 30788 RVA: 0x001BC4F1 File Offset: 0x001BA6F1
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = (double)this.sum;
			return true;
		}

		// Token: 0x040020E4 RID: 8420
		private long sum;
	}
}
