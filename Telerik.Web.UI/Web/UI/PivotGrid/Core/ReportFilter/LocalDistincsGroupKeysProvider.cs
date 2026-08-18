using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.ReportFilter
{
	// Token: 0x020006D5 RID: 1749
	internal class LocalDistincsGroupKeysProvider : DistinctValuesProvider
	{
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000C7EE8 File Offset: 0x000C60E8
		public LocalDistincsGroupKeysProvider(IDataProvider provider, GroupDescription groupDescription)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (groupDescription == null)
			{
				throw new ArgumentNullException("groupDescription");
			}
			this.provider = provider;
			this.description = groupDescription;
			this.disctinctValues = new List<object>();
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000C7F25 File Offset: 0x000C6125
		public override IEnumerable<object> DisctinctValues
		{
			get
			{
				return this.disctinctValues;
			}
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000C7F30 File Offset: 0x000C6130
		public override void Refresh()
		{
			int num = this.provider.Settings.RowGroupDescriptions.IndexOf(this.description);
			PivotAxis axis = PivotAxis.Rows;
			if (num < 0)
			{
				num = this.provider.Settings.ColumnGroupDescriptions.IndexOf(this.description);
				axis = PivotAxis.Columns;
			}
			if (num < 0)
			{
				return;
			}
			IEnumerable<object> uniqueKeys = this.provider.Results.GetUniqueKeys(axis, num);
			if (uniqueKeys != null)
			{
				List<object> list = uniqueKeys.ToList<object>();
				list.Sort(new LocalDistincsGroupKeysProvider.MyCustomComparer());
				this.disctinctValues = list;
			}
			base.OnUpdated();
		}

		// Token: 0x040010AD RID: 4269
		private readonly IDataProvider provider;

		// Token: 0x040010AE RID: 4270
		private readonly GroupDescription description;

		// Token: 0x040010AF RID: 4271
		private IEnumerable<object> disctinctValues;

		// Token: 0x020006D6 RID: 1750
		private class MyCustomComparer : IComparer<object>
		{
			// Token: 0x06003EB4 RID: 16052 RVA: 0x000C7FB8 File Offset: 0x000C61B8
			public int Compare(object x, object y)
			{
				if (x == NullValue.Instance && y == NullValue.Instance)
				{
					return 0;
				}
				if (x == NullValue.Instance)
				{
					return -1;
				}
				if (y == NullValue.Instance)
				{
					return 1;
				}
				IComparable comparable = x as IComparable;
				IComparable comparable2 = y as IComparable;
				if (comparable != null && comparable2 != null)
				{
					return comparable.CompareTo(comparable2);
				}
				return 0;
			}
		}
	}
}
