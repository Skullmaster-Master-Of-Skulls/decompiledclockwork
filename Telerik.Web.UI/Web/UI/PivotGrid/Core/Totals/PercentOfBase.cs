using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C6A RID: 3178
	[DataContract]
	public abstract class PercentOfBase : ComparedTo
	{
		// Token: 0x060077AD RID: 30637 RVA: 0x001BB366 File Offset: 0x001B9566
		public override string GetStringFormat(Type dataType, string stringFormat)
		{
			return "0.00%";
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x001BB370 File Offset: 0x001B9570
		internal override AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex)
		{
			if (current != null)
			{
				if (index == 0)
				{
					return new ConstantValueAggregate(1.0);
				}
				if (comparison != null)
				{
					return new ConstantValueAggregate(current.Value / comparison.Value);
				}
			}
			else if (comparison != null)
			{
				return new ConstantValueAggregate(0.0);
			}
			return null;
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x001BB3DE File Offset: 0x001B95DE
		internal override double? Accumulate(double? aggregate, double? current, int index, int count, int baseIndex)
		{
			return current;
		}
	}
}
