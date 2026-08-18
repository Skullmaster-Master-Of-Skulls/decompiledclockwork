using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C63 RID: 3171
	internal class DoubleAggregateValueComparer : TotalComparer
	{
		// Token: 0x0600778F RID: 30607 RVA: 0x001BAF8C File Offset: 0x001B918C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		public override int Compare(TotalValue x, TotalValue y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			if (x.Value == null && y.Value == null)
			{
				return 0;
			}
			if (x.Value == null)
			{
				return -1;
			}
			if (y.Value == null)
			{
				return 1;
			}
			int result;
			try
			{
				double num = Convert.ToDouble(x.Value.GetValue(), CultureInfo.CurrentCulture);
				try
				{
					double value = Convert.ToDouble(y.Value.GetValue(), CultureInfo.CurrentCulture);
					result = num.CompareTo(value);
				}
				catch
				{
					result = 1;
				}
			}
			catch
			{
				try
				{
					result = -1;
				}
				catch
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x001BB040 File Offset: 0x001B9240
		protected override Cloneable CreateInstanceCore()
		{
			return new DoubleAggregateValueComparer();
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x001BB047 File Offset: 0x001B9247
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
