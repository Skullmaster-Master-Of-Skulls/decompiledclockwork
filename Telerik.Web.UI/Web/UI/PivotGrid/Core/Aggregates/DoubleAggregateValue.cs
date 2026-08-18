using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x0200068F RID: 1679
	public sealed class DoubleAggregateValue : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06003D04 RID: 15620 RVA: 0x000C4ADA File Offset: 0x000C2CDA
		public DoubleAggregateValue(double value)
		{
			this.result = value;
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000C4AE9 File Offset: 0x000C2CE9
		protected override object GetValueOverride()
		{
			return this.result;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x000C4AF6 File Offset: 0x000C2CF6
		protected override void AccumulateOverride(object value)
		{
			base.RaiseError();
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x000C4AFE File Offset: 0x000C2CFE
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			base.RaiseError();
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000C4B06 File Offset: 0x000C2D06
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.result;
			return true;
		}

		// Token: 0x04001057 RID: 4183
		private double result;
	}
}
