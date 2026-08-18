using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C77 RID: 3191
	internal sealed class AverageAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x060077F0 RID: 30704 RVA: 0x001BBA36 File Offset: 0x001B9C36
		internal override AggregateError Error
		{
			get
			{
				if (this.count > 0)
				{
					return base.Error;
				}
				return AggregateValue.AggregateError;
			}
		}

		// Token: 0x060077F1 RID: 30705 RVA: 0x001BBA4D File Offset: 0x001B9C4D
		protected override object GetValueOverride()
		{
			return this.sum / (double)this.count;
		}

		// Token: 0x060077F2 RID: 30706 RVA: 0x001BBA62 File Offset: 0x001B9C62
		protected override void AccumulateOverride(object value)
		{
			this.sum += Convert.ToDouble(value, CultureInfo.InvariantCulture);
			this.count++;
		}

		// Token: 0x060077F3 RID: 30707 RVA: 0x001BBA8C File Offset: 0x001B9C8C
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			AverageAggregate averageAggregate = childAggregate as AverageAggregate;
			if (averageAggregate != null)
			{
				this.sum += averageAggregate.sum;
				this.count += averageAggregate.count;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x001BBAD0 File Offset: 0x001B9CD0
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.sum / (double)this.count;
			return true;
		}

		// Token: 0x040020D4 RID: 8404
		private double sum;

		// Token: 0x040020D5 RID: 8405
		private int count;
	}
}
