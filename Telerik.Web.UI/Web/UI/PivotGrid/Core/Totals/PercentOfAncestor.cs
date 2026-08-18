using System;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C6C RID: 3180
	[DataContract]
	public abstract class PercentOfAncestor : SingleTotalFormat
	{
		// Token: 0x060077B9 RID: 30649 RVA: 0x001BB499 File Offset: 0x001B9699
		internal PercentOfAncestor()
		{
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x001BB4A1 File Offset: 0x001B96A1
		public override string GetStringFormat(Type dataType, string stringFormat)
		{
			return "0.00%";
		}

		// Token: 0x060077BB RID: 30651
		internal abstract Coordinate OfGroup(Coordinate valueGroups, Coordinate rootGroups);

		// Token: 0x060077BC RID: 30652 RVA: 0x001BB4A8 File Offset: 0x001B96A8
		internal sealed override AggregateValue FormatValue(Coordinate groups, IAggregateResultProvider results, int aggregateIndex)
		{
			AggregateValue aggregateResult = results.GetAggregateResult(aggregateIndex, groups);
			double num;
			if (aggregateResult == null)
			{
				num = 0.0;
			}
			else
			{
				num = Convert.ToDouble(aggregateResult.GetValue(), CultureInfo.InvariantCulture);
			}
			AggregateValue aggregateResult2 = results.GetAggregateResult(aggregateIndex, this.OfGroup(groups, results.Root));
			double num2;
			if (aggregateResult2 == null)
			{
				num2 = 0.0;
			}
			else
			{
				num2 = Convert.ToDouble(aggregateResult2.GetValue(), CultureInfo.InvariantCulture);
			}
			return new ConstantValueAggregate(num / num2);
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x001BB520 File Offset: 0x001B9720
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
