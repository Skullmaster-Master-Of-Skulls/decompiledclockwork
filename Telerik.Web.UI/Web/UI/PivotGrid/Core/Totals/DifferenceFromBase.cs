using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C5E RID: 3166
	[DataContract]
	public abstract class DifferenceFromBase : ComparedTo
	{
		// Token: 0x0600777C RID: 30588 RVA: 0x001BAE22 File Offset: 0x001B9022
		internal DifferenceFromBase()
		{
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x001BAE2A File Offset: 0x001B902A
		internal override AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex)
		{
			if (index == 0)
			{
				return null;
			}
			if (current != null)
			{
				return new ConstantValueAggregate(current.Value - comparison.Value);
			}
			return new ConstantValueAggregate(-comparison.Value);
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x001BAE66 File Offset: 0x001B9066
		internal override double? Accumulate(double? aggregate, double? current, int index, int count, int baseIndex)
		{
			return new double?((current != null) ? current.Value : 0.0);
		}

		// Token: 0x0600777F RID: 30591 RVA: 0x001BAE88 File Offset: 0x001B9088
		protected override Cloneable CreateInstanceCore()
		{
			throw new NotImplementedException();
		}
	}
}
