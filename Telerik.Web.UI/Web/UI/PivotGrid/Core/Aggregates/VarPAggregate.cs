using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C89 RID: 3209
	internal sealed class VarPAggregate : VarianceAggregateBase, IConvertibleAggregateValue<double>
	{
		// Token: 0x170026D9 RID: 9945
		// (get) Token: 0x0600784C RID: 30796 RVA: 0x001BC59F File Offset: 0x001BA79F
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

		// Token: 0x0600784D RID: 30797 RVA: 0x001BC5B8 File Offset: 0x001BA7B8
		protected override object GetValueOverride()
		{
			double num = this.ComputeVariance();
			return num;
		}

		// Token: 0x0600784E RID: 30798 RVA: 0x001BC5D4 File Offset: 0x001BA7D4
		private double ComputeVariance()
		{
			return base.GetSquaredDifferencesSum() / (double)base.Count;
		}

		// Token: 0x0600784F RID: 30799 RVA: 0x001BC5F1 File Offset: 0x001BA7F1
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.ComputeVariance();
			return true;
		}
	}
}
