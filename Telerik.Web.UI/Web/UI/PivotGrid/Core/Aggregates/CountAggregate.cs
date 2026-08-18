using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C7A RID: 3194
	internal sealed class CountAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007800 RID: 30720 RVA: 0x001BBC13 File Offset: 0x001B9E13
		protected override object GetValueOverride()
		{
			return this.count;
		}

		// Token: 0x06007801 RID: 30721 RVA: 0x001BBC20 File Offset: 0x001B9E20
		protected override void AccumulateOverride(object item)
		{
			this.count += 1UL;
		}

		// Token: 0x06007802 RID: 30722 RVA: 0x001BBC34 File Offset: 0x001B9E34
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			CountAggregate countAggregate = childAggregate as CountAggregate;
			if (countAggregate != null)
			{
				this.count += countAggregate.count;
				return;
			}
			ulong num;
			if (childAggregate.TryConvertValue(out num))
			{
				this.count += num;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x06007803 RID: 30723 RVA: 0x001BBC7E File Offset: 0x001B9E7E
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

		// Token: 0x040020D9 RID: 8409
		private ulong count;
	}
}
