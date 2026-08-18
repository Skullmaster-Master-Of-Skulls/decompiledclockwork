using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C84 RID: 3204
	internal sealed class StdDevPAggregate : VarianceAggregateBase, IConvertibleAggregateValue<double>
	{
		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x06007832 RID: 30770 RVA: 0x001BC2EC File Offset: 0x001BA4EC
		internal override AggregateError Error
		{
			get
			{
				if (base.Count > 0)
				{
					return base.Error;
				}
				return AggregateValue.AggregateError;
			}
		}

		// Token: 0x06007833 RID: 30771 RVA: 0x001BC304 File Offset: 0x001BA504
		protected override object GetValueOverride()
		{
			double num = this.ComputeStandardDeviationFromPopulation();
			return num;
		}

		// Token: 0x06007834 RID: 30772 RVA: 0x001BC320 File Offset: 0x001BA520
		private double ComputeStandardDeviationFromPopulation()
		{
			return Math.Sqrt(base.GetSquaredDifferencesSum() / (double)base.Count);
		}

		// Token: 0x06007835 RID: 30773 RVA: 0x001BC342 File Offset: 0x001BA542
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.ComputeStandardDeviationFromPopulation();
			return true;
		}
	}
}
