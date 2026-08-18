using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C7F RID: 3199
	internal sealed class MinDecimalAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06007819 RID: 30745 RVA: 0x001BBF4D File Offset: 0x001BA14D
		protected override object GetValueOverride()
		{
			return this.min;
		}

		// Token: 0x0600781A RID: 30746 RVA: 0x001BBF5A File Offset: 0x001BA15A
		protected override void AccumulateOverride(object value)
		{
			this.min = Math.Min(this.min, Convert.ToDecimal(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600781B RID: 30747 RVA: 0x001BBF78 File Offset: 0x001BA178
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			MinDecimalAggregate minDecimalAggregate = childAggregate as MinDecimalAggregate;
			if (minDecimalAggregate != null)
			{
				this.min = Math.Min(this.min, minDecimalAggregate.min);
				return;
			}
			base.RaiseError();
		}

		// Token: 0x0600781C RID: 30748 RVA: 0x001BBFAD File Offset: 0x001BA1AD
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = (double)this.min;
			return true;
		}

		// Token: 0x040020DE RID: 8414
		private decimal min = decimal.MaxValue;
	}
}
