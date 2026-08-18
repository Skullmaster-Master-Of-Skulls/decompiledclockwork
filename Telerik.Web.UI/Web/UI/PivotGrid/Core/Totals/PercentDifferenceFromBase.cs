using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C66 RID: 3174
	[DataContract]
	public abstract class PercentDifferenceFromBase : DifferenceFromBase
	{
		// Token: 0x0600779D RID: 30621 RVA: 0x001BB18F File Offset: 0x001B938F
		internal PercentDifferenceFromBase()
		{
		}

		// Token: 0x0600779E RID: 30622 RVA: 0x001BB198 File Offset: 0x001B9398
		internal override AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex)
		{
			if (index == 0)
			{
				return null;
			}
			if (current != null)
			{
				if (comparison != null && comparison.Value != 0.0)
				{
					return new ConstantValueAggregate((current.Value - comparison.Value) / comparison.Value);
				}
			}
			else if (comparison != null && comparison.Value != 0.0)
			{
				return new ConstantValueAggregate(-1.0);
			}
			return null;
		}

		// Token: 0x0600779F RID: 30623 RVA: 0x001BB220 File Offset: 0x001B9420
		public override string GetStringFormat(Type dataType, string stringFormat)
		{
			if (PrecisionHelpers.GetPrecision(dataType) == Precision.Unknown)
			{
				return stringFormat;
			}
			return "0.00%";
		}
	}
}
