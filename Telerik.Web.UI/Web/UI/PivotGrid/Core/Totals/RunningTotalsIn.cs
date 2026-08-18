using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C74 RID: 3188
	[DataContract]
	public sealed class RunningTotalsIn : SiblingTotalsFormat
	{
		// Token: 0x060077E7 RID: 30695 RVA: 0x001BB938 File Offset: 0x001B9B38
		internal override void FormatTotals(IReadOnlyList<TotalValue> valueFormatters, IAggregateResultProvider results)
		{
			double num = 0.0;
			foreach (TotalValue totalValue in valueFormatters)
			{
				AggregateValue value = totalValue.Value;
				if (value != null)
				{
					num += Convert.ToDouble(value.GetValue(), CultureInfo.InvariantCulture);
				}
				totalValue.FormattedValue = new ConstantValueAggregate(num);
			}
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x001BB9B4 File Offset: 0x001B9BB4
		protected override Cloneable CreateInstanceCore()
		{
			return new RunningTotalsIn();
		}
	}
}
