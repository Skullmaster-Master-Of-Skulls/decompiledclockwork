using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C78 RID: 3192
	internal sealed class AverageDecimalAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x060077F6 RID: 30710 RVA: 0x001BBB00 File Offset: 0x001B9D00
		internal override AggregateError Error
		{
			get
			{
				if (this.count > 0U)
				{
					return base.Error;
				}
				return AggregateValue.AggregateError;
			}
		}

		// Token: 0x060077F7 RID: 30711 RVA: 0x001BBB17 File Offset: 0x001B9D17
		protected override object GetValueOverride()
		{
			return this.sum / this.count;
		}

		// Token: 0x060077F8 RID: 30712 RVA: 0x001BBB34 File Offset: 0x001B9D34
		protected override void AccumulateOverride(object value)
		{
			this.sum += Convert.ToDecimal(value, CultureInfo.InvariantCulture);
			this.count += 1U;
		}

		// Token: 0x060077F9 RID: 30713 RVA: 0x001BBB60 File Offset: 0x001B9D60
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			AverageDecimalAggregate averageDecimalAggregate = childAggregate as AverageDecimalAggregate;
			if (averageDecimalAggregate != null)
			{
				this.sum += averageDecimalAggregate.sum;
				this.count += averageDecimalAggregate.count;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x060077FA RID: 30714 RVA: 0x001BBBA8 File Offset: 0x001B9DA8
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = (double)this.sum / this.count;
			return true;
		}

		// Token: 0x040020D6 RID: 8406
		private decimal sum;

		// Token: 0x040020D7 RID: 8407
		private uint count;
	}
}
