using System;

namespace Telerik.Web.UI
{
	// Token: 0x020000C5 RID: 197
	public class RadProxyBoundControlPagingManager
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x0001C9D7 File Offset: 0x0001ABD7
		public RadProxyBoundControlPagingManager(ProxyBoundControlEnumerableBase enumerableBase)
		{
			this._enumerable = enumerableBase;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001C9E6 File Offset: 0x0001ABE6
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001CA02 File Offset: 0x0001AC02
		public int Count
		{
			get
			{
				return this._enumerable.Count;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001CA0F File Offset: 0x0001AC0F
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0001CA17 File Offset: 0x0001AC17
		public int CurrentPageIndex { get; internal set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001CA20 File Offset: 0x0001AC20
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public int PageSize { get; internal set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001CA31 File Offset: 0x0001AC31
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x0001CA39 File Offset: 0x0001AC39
		public bool AllowPaging { get; internal set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001CA42 File Offset: 0x0001AC42
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x0001CA4A File Offset: 0x0001AC4A
		public bool AllowCustomPaging { get; internal set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0001CA53 File Offset: 0x0001AC53
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x0001CA5B File Offset: 0x0001AC5B
		public int VirtualItemCount { get; internal set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001CA64 File Offset: 0x0001AC64
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001CAA5 File Offset: 0x0001ACA5
		public bool IsPagingEnabled
		{
			get
			{
				return this._enumerable.SupportsPaging && this.AllowPaging && this.PageSize != 0;
			}
		}

		// Token: 0x040001BC RID: 444
		private readonly ProxyBoundControlEnumerableBase _enumerable;
	}
}
