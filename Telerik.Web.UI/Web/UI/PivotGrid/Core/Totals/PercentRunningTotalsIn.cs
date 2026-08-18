using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C72 RID: 3186
	[DataContract]
	public sealed class PercentRunningTotalsIn : SiblingTotalsFormat
	{
		// Token: 0x060077D7 RID: 30679 RVA: 0x001BB6A6 File Offset: 0x001B98A6
		public override string GetStringFormat(Type dataType, string stringFormat)
		{
			return "0.00%";
		}

		// Token: 0x060077D8 RID: 30680 RVA: 0x001BB6B0 File Offset: 0x001B98B0
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
			}
			double num2 = 0.0;
			foreach (TotalValue totalValue2 in valueFormatters)
			{
				AggregateValue value2 = totalValue2.Value;
				if (value2 != null)
				{
					num2 += Convert.ToDouble(value2.GetValue(), CultureInfo.InvariantCulture);
				}
				totalValue2.FormattedValue = new ConstantValueAggregate(num2 / num);
			}
		}

		// Token: 0x060077D9 RID: 30681 RVA: 0x001BB794 File Offset: 0x001B9994
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentRunningTotalsIn();
		}
	}
}
