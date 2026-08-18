using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C81 RID: 3201
	internal sealed class ProductAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007823 RID: 30755 RVA: 0x001BC085 File Offset: 0x001BA285
		protected override object GetValueOverride()
		{
			return this.product;
		}

		// Token: 0x06007824 RID: 30756 RVA: 0x001BC092 File Offset: 0x001BA292
		protected override void AccumulateOverride(object value)
		{
			this.product *= Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06007825 RID: 30757 RVA: 0x001BC0AC File Offset: 0x001BA2AC
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			ProductAggregate productAggregate = childAggregate as ProductAggregate;
			if (productAggregate != null)
			{
				this.product *= productAggregate.product;
				return;
			}
			double num;
			if (childAggregate.TryConvertValue(out num))
			{
				this.product *= num;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007826 RID: 30758 RVA: 0x001BC0F6 File Offset: 0x001BA2F6
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.product;
			return true;
		}

		// Token: 0x040020E0 RID: 8416
		private double product = 1.0;
	}
}
