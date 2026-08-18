using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006D7 RID: 1751
	internal class DefaultAggregateValueComparer : IComparer<AggregateValue>
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000C8010 File Offset: 0x000C6210
		public int Compare(AggregateValue x, AggregateValue y)
		{
			bool flag = x == null;
			bool flag2 = y == null;
			if (flag && flag2)
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			if (flag2)
			{
				return 1;
			}
			object value = x.GetValue();
			object value2 = y.GetValue();
			bool flag3 = value is AggregateError;
			bool flag4 = value2 is AggregateError;
			if (!flag3 || !flag4)
			{
				if (flag3)
				{
					return -1;
				}
				if (flag4)
				{
					return 1;
				}
			}
			IComparable comparable = value as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(value2);
			}
			IComparable comparable2 = value2 as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(value);
			}
			return 0;
		}
	}
}
