using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C85 RID: 3205
	internal sealed class SumAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007837 RID: 30775 RVA: 0x001BC36A File Offset: 0x001BA56A
		protected override object GetValueOverride()
		{
			return this.sum;
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x001BC377 File Offset: 0x001BA577
		protected override void AccumulateOverride(object value)
		{
			this.sum += Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x001BC394 File Offset: 0x001BA594
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			SumAggregate sumAggregate = childAggregate as SumAggregate;
			if (sumAggregate != null)
			{
				this.sum += sumAggregate.sum;
				return;
			}
			double num;
			if (childAggregate.TryConvertValue(out num))
			{
				this.sum += num;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x001BC3DE File Offset: 0x001BA5DE
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.sum;
			return true;
		}

		// Token: 0x040020E2 RID: 8418
		private double sum;
	}
}
