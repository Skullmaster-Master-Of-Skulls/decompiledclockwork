using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C88 RID: 3208
	internal sealed class VarAggregate : VarianceAggregateBase, IConvertibleAggregateValue<double>
	{
		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x06007847 RID: 30791 RVA: 0x001BC522 File Offset: 0x001BA722
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

		// Token: 0x06007848 RID: 30792 RVA: 0x001BC53C File Offset: 0x001BA73C
		protected override object GetValueOverride()
		{
			double num = this.ComputeVariance();
			return num;
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x001BC558 File Offset: 0x001BA758
		private double ComputeVariance()
		{
			return base.GetSquaredDifferencesSum() / (double)(base.Count - 1);
		}

		// Token: 0x0600784A RID: 30794 RVA: 0x001BC577 File Offset: 0x001BA777
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
