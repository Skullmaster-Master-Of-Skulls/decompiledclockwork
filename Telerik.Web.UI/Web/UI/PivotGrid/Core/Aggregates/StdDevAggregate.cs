using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C83 RID: 3203
	internal sealed class StdDevAggregate : VarianceAggregateBase, IConvertibleAggregateValue<double>
	{
		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x0600782D RID: 30765 RVA: 0x001BC25C File Offset: 0x001BA45C
		internal override AggregateError Error
		{
			get
			{
				if (base.Count > 1)
				{
					return base.Error;
				}
				return AggregateValue.AggregateError;
			}
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x001BC274 File Offset: 0x001BA474
		protected override object GetValueOverride()
		{
			double num = Math.Sqrt(base.GetSquaredDifferencesSum() / (double)(base.Count - 1));
			return num;
		}

		// Token: 0x0600782F RID: 30767 RVA: 0x001BC2A0 File Offset: 0x001BA4A0
		private double ComputeStandardDeviation()
		{
			return Math.Sqrt(base.GetSquaredDifferencesSum() / (double)(base.Count - 1));
		}

		// Token: 0x06007830 RID: 30768 RVA: 0x001BC2C4 File Offset: 0x001BA4C4
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.ComputeStandardDeviation();
			return true;
		}
	}
}
