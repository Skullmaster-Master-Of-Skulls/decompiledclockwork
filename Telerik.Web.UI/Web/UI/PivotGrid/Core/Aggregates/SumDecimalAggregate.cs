using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C86 RID: 3206
	internal sealed class SumDecimalAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x0600783C RID: 30780 RVA: 0x001BC407 File Offset: 0x001BA607
		protected override object GetValueOverride()
		{
			return this.sum;
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x001BC414 File Offset: 0x001BA614
		protected override void AccumulateOverride(object value)
		{
			this.sum += Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x001BC434 File Offset: 0x001BA634
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			SumDecimalAggregate sumDecimalAggregate = childAggregate as SumDecimalAggregate;
			if (sumDecimalAggregate != null)
			{
				this.sum += sumDecimalAggregate.sum;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x001BC469 File Offset: 0x001BA669
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

		// Token: 0x040020E3 RID: 8419
		private decimal sum;
	}
}
