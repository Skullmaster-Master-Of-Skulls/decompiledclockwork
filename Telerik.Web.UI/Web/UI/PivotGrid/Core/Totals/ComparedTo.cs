using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C5C RID: 3164
	[DataContract]
	public abstract class ComparedTo : SiblingTotalsFormat
	{
		// Token: 0x06007771 RID: 30577 RVA: 0x001BAC11 File Offset: 0x001B8E11
		internal ComparedTo()
		{
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x001BAC1C File Offset: 0x001B8E1C
		internal sealed override void FormatTotals(IReadOnlyList<TotalValue> valueFormatters, IAggregateResultProvider results)
		{
			object baseGroupName = this.BaseGroupName();
			int num = this.BaseTotalIndex(valueFormatters, baseGroupName);
			double? accumulation = ComparedTo.DoubleValueAtIndex(valueFormatters, baseGroupName, num);
			int count = valueFormatters.Count;
			switch (this.GetItteration())
			{
			case ComparedToItteration.Forward:
				for (int i = 0; i < count; i++)
				{
					TotalValue total = valueFormatters[i];
					accumulation = this.FormatAndAccumulate(accumulation, count, i, total, num);
				}
				return;
			case ComparedToItteration.Backward:
			{
				int num2 = count - 1;
				for (int j = num2; j >= 0; j--)
				{
					TotalValue total2 = valueFormatters[j];
					accumulation = this.FormatAndAccumulate(accumulation, count, num2 - j, total2, num2 - num);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06007773 RID: 30579 RVA: 0x001BACC0 File Offset: 0x001B8EC0
		private static double? DoubleValueAtIndex(IReadOnlyList<TotalValue> valueFormatters, object baseGroupName, int baseIndex)
		{
			double? result = null;
			if (baseIndex >= 0)
			{
				TotalValue totalValue = valueFormatters[baseIndex];
				if (totalValue.Value != null)
				{
					result = new double?(Convert.ToDouble(totalValue.Value.GetValue(), CultureInfo.CurrentCulture));
				}
			}
			else if (baseGroupName != null)
			{
				return new double?(double.NaN);
			}
			return result;
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x001BAD1A File Offset: 0x001B8F1A
		internal virtual ComparedToItteration GetItteration()
		{
			return ComparedToItteration.Forward;
		}

		// Token: 0x06007775 RID: 30581
		internal abstract AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex);

		// Token: 0x06007776 RID: 30582
		internal abstract double? Accumulate(double? aggregate, double? current, int index, int count, int baseIndex);

		// Token: 0x06007777 RID: 30583 RVA: 0x001BAD1D File Offset: 0x001B8F1D
		internal virtual object BaseGroupName()
		{
			return null;
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x001BAD20 File Offset: 0x001B8F20
		private int BaseTotalIndex(IReadOnlyList<TotalValue> valueFormatters, object baseGroupName)
		{
			if (baseGroupName != null)
			{
				for (int i = 0; i < valueFormatters.Count; i++)
				{
					TotalValue totalValue = valueFormatters[i];
					if (this.HasGroupNameAt(totalValue.Groups, baseGroupName))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06007779 RID: 30585 RVA: 0x001BAD5C File Offset: 0x001B8F5C
		private bool HasGroupNameAt(Coordinate groups, object groupName)
		{
			IGroup group = (base.Axis == PivotAxis.Rows) ? groups.RowGroup : groups.ColumnGroup;
			int num = group.Level - 1;
			int num2 = num - base.Level;
			IGroup group2 = group;
			while (num2-- > 0)
			{
				group2 = group2.Parent;
			}
			return object.Equals(group2.Name, groupName);
		}

		// Token: 0x0600777A RID: 30586 RVA: 0x001BADB4 File Offset: 0x001B8FB4
		private double? FormatAndAccumulate(double? accumulation, int count, int index, TotalValue total, int baseIndex)
		{
			double? doubleValue = ComparedTo.GetDoubleValue(total);
			total.FormattedValue = this.Format(accumulation, doubleValue, index, count, baseIndex);
			return this.Accumulate(accumulation, doubleValue, index, count, baseIndex);
		}

		// Token: 0x0600777B RID: 30587 RVA: 0x001BADE8 File Offset: 0x001B8FE8
		private static double? GetDoubleValue(TotalValue total)
		{
			double? result = null;
			if (total.Value != null)
			{
				result = new double?(Convert.ToDouble(total.Value.GetValue(), CultureInfo.CurrentCulture));
			}
			return result;
		}
	}
}
