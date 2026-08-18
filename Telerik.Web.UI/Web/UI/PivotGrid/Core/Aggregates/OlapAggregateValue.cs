using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000690 RID: 1680
	internal sealed class OlapAggregateValue : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06003D09 RID: 15625 RVA: 0x000C4B26 File Offset: 0x000C2D26
		protected override object GetValueOverride()
		{
			return this.aggregateValue;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000C4B33 File Offset: 0x000C2D33
		protected override void AccumulateOverride(object value)
		{
			if (value is string)
			{
				return;
			}
			this.aggregateValue = Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000C4B4F File Offset: 0x000C2D4F
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			this.aggregateValue += ((OlapAggregateValue)childAggregate).aggregateValue;
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000C4B69 File Offset: 0x000C2D69
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.aggregateValue;
			return true;
		}

		// Token: 0x04001058 RID: 4184
		private double aggregateValue;
	}
}
