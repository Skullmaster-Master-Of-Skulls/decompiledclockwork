using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x0200068E RID: 1678
	internal sealed class CountDoubleAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06003CFF RID: 15615 RVA: 0x000C4A41 File Offset: 0x000C2C41
		protected override object GetValueOverride()
		{
			return this.count;
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000C4A4E File Offset: 0x000C2C4E
		protected override void AccumulateOverride(object item)
		{
			this.count += 1.0;
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000C4A68 File Offset: 0x000C2C68
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			CountDoubleAggregate countDoubleAggregate = childAggregate as CountDoubleAggregate;
			if (countDoubleAggregate != null)
			{
				this.count += countDoubleAggregate.count;
				return;
			}
			double num;
			if (childAggregate.TryConvertValue(out num))
			{
				this.count += num;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000C4AB2 File Offset: 0x000C2CB2
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.count;
			return true;
		}

		// Token: 0x04001056 RID: 4182
		private double count;
	}
}
