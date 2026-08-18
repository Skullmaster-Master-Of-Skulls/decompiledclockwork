using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Queryable.Descriptions;

namespace Telerik.Web.UI.PivotGrid.Queryable.Aggregates
{
	// Token: 0x02000726 RID: 1830
	internal sealed class QueryableAverageAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x060040E5 RID: 16613 RVA: 0x000CC574 File Offset: 0x000CA774
		protected override object GetValueOverride()
		{
			object result = null;
			if (this.count != 0)
			{
				result = this.sum / (double)this.count;
			}
			return result;
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000CC5A0 File Offset: 0x000CA7A0
		protected override void AccumulateOverride(object value)
		{
			QueryableAverageResult queryableAverageResult = (QueryableAverageResult)value;
			if (queryableAverageResult.Sum != null && queryableAverageResult.Count != null)
			{
				this.sum += queryableAverageResult.Sum.Value;
				this.count += queryableAverageResult.Count.Value;
			}
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000CC60C File Offset: 0x000CA80C
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			QueryableAverageAggregate queryableAverageAggregate = childAggregate as QueryableAverageAggregate;
			if (queryableAverageAggregate != null)
			{
				this.sum += queryableAverageAggregate.sum;
				this.count += queryableAverageAggregate.count;
				return;
			}
			base.RaiseError();
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000CC650 File Offset: 0x000CA850
		bool IConvertibleAggregateValue<double>.TryConvertValue(out double value)
		{
			if (base.IsError)
			{
				value = 0.0;
				return false;
			}
			value = this.sum / (double)this.count;
			return true;
		}

		// Token: 0x04001139 RID: 4409
		private double sum;

		// Token: 0x0400113A RID: 4410
		private int count;
	}
}
