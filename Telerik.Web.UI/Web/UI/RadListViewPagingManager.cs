using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019A2 RID: 6562
	public class RadListViewPagingManager
	{
		// Token: 0x0600FDCF RID: 64975 RVA: 0x0038FDED File Offset: 0x0038DFED
		public RadListViewPagingManager(ListViewEnumerableBase enumerableBase)
		{
			this._enumerable = enumerableBase;
		}

		// Token: 0x17004CA1 RID: 19617
		// (get) Token: 0x0600FDD0 RID: 64976 RVA: 0x0038FDFC File Offset: 0x0038DFFC
		public int DataSourceCount
		{
			get
			{
				if (this.AllowCustomPaging)
				{
					return this.VirtualItemCount;
				}
				return this._enumerable.DataSourceCount;
			}
		}

		// Token: 0x17004CA2 RID: 19618
		// (get) Token: 0x0600FDD1 RID: 64977 RVA: 0x0038FE18 File Offset: 0x0038E018
		public int Count
		{
			get
			{
				return this._enumerable.Count;
			}
		}

		// Token: 0x17004CA3 RID: 19619
		// (get) Token: 0x0600FDD2 RID: 64978 RVA: 0x0038FE25 File Offset: 0x0038E025
		// (set) Token: 0x0600FDD3 RID: 64979 RVA: 0x0038FE2D File Offset: 0x0038E02D
		public int CurrentPageIndex { get; internal set; }

		// Token: 0x17004CA4 RID: 19620
		// (get) Token: 0x0600FDD4 RID: 64980 RVA: 0x0038FE36 File Offset: 0x0038E036
		// (set) Token: 0x0600FDD5 RID: 64981 RVA: 0x0038FE3E File Offset: 0x0038E03E
		public int PageSize { get; internal set; }

		// Token: 0x17004CA5 RID: 19621
		// (get) Token: 0x0600FDD6 RID: 64982 RVA: 0x0038FE47 File Offset: 0x0038E047
		// (set) Token: 0x0600FDD7 RID: 64983 RVA: 0x0038FE4F File Offset: 0x0038E04F
		public bool AllowPaging { get; internal set; }

		// Token: 0x17004CA6 RID: 19622
		// (get) Token: 0x0600FDD8 RID: 64984 RVA: 0x0038FE58 File Offset: 0x0038E058
		// (set) Token: 0x0600FDD9 RID: 64985 RVA: 0x0038FE60 File Offset: 0x0038E060
		public bool AllowCustomPaging { get; internal set; }

		// Token: 0x17004CA7 RID: 19623
		// (get) Token: 0x0600FDDA RID: 64986 RVA: 0x0038FE69 File Offset: 0x0038E069
		// (set) Token: 0x0600FDDB RID: 64987 RVA: 0x0038FE71 File Offset: 0x0038E071
		public int VirtualItemCount { get; internal set; }

		// Token: 0x17004CA8 RID: 19624
		// (get) Token: 0x0600FDDC RID: 64988 RVA: 0x0038FE7C File Offset: 0x0038E07C
		public int PageCount
		{
			get
			{
				if (!this._enumerable.SupportsPaging)
				{
					return 1;
				}
				int dataSourceCount = this.DataSourceCount;
				if (this.IsPagingEnabled && dataSourceCount != 0)
				{
					return (dataSourceCount + this.PageSize - 1) / this.PageSize;
				}
				return 1;
			}
		}

		// Token: 0x17004CA9 RID: 19625
		// (get) Token: 0x0600FDDD RID: 64989 RVA: 0x0038FEBD File Offset: 0x0038E0BD
		public bool IsPagingEnabled
		{
			get
			{
				return this._enumerable.SupportsPaging && this.AllowPaging && this.PageSize != 0;
			}
		}

		// Token: 0x04004808 RID: 18440
		private readonly ListViewEnumerableBase _enumerable;
	}
}
