using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Queryable.Aggregates
{
	// Token: 0x02000729 RID: 1833
	internal sealed class QueryableCountAggregate : AggregateValue, IConvertibleAggregateValue<double>
	{
		// Token: 0x06004101 RID: 16641 RVA: 0x000CC882 File Offset: 0x000CAA82
		protected override object GetValueOverride()
		{
			return this.count;
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000CC88F File Offset: 0x000CAA8F
		protected override void AccumulateOverride(object item)
		{
			this.count += (ulong)((long)((int)item));
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000CC8A8 File Offset: 0x000CAAA8
		protected override void MergeOverride(AggregateValue childAggregate)
		{
			QueryableCountAggregate queryableCountAggregate = childAggregate as QueryableCountAggregate;
			if (queryableCountAggregate != null)
			{
				this.count += queryableCountAggregate.count;
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

		// Token: 0x06004104 RID: 16644 RVA: 0x000CC8F2 File Offset: 0x000CAAF2
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

		// Token: 0x04001140 RID: 4416
		private ulong count;
	}
}
