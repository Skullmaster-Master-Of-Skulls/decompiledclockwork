using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DFD RID: 3581
	public class PivotGridPagingManager
	{
		// Token: 0x060084F2 RID: 34034 RVA: 0x001E6176 File Offset: 0x001E4376
		public PivotGridPagingManager(int dataSourceCount)
		{
			this._dataSourceCount = dataSourceCount;
		}

		// Token: 0x17002A04 RID: 10756
		// (get) Token: 0x060084F3 RID: 34035 RVA: 0x001E6185 File Offset: 0x001E4385
		public int DataSourceCount
		{
			get
			{
				return this._dataSourceCount;
			}
		}

		// Token: 0x17002A05 RID: 10757
		// (get) Token: 0x060084F4 RID: 34036 RVA: 0x001E618D File Offset: 0x001E438D
		public int Count
		{
			get
			{
				return this._dataSourceCount;
			}
		}

		// Token: 0x17002A06 RID: 10758
		// (get) Token: 0x060084F5 RID: 34037 RVA: 0x001E6195 File Offset: 0x001E4395
		// (set) Token: 0x060084F6 RID: 34038 RVA: 0x001E619D File Offset: 0x001E439D
		public int CurrentPageIndex { get; internal set; }

		// Token: 0x17002A07 RID: 10759
		// (get) Token: 0x060084F7 RID: 34039 RVA: 0x001E61A6 File Offset: 0x001E43A6
		// (set) Token: 0x060084F8 RID: 34040 RVA: 0x001E61AE File Offset: 0x001E43AE
		public int PageSize { get; internal set; }

		// Token: 0x17002A08 RID: 10760
		// (get) Token: 0x060084F9 RID: 34041 RVA: 0x001E61B7 File Offset: 0x001E43B7
		// (set) Token: 0x060084FA RID: 34042 RVA: 0x001E61BF File Offset: 0x001E43BF
		public bool AllowPaging { get; internal set; }

		// Token: 0x17002A09 RID: 10761
		// (get) Token: 0x060084FB RID: 34043 RVA: 0x001E61C8 File Offset: 0x001E43C8
		public int FirstIndexInPage
		{
			get
			{
				if (!this.IsPagingEnabled)
				{
					return 0;
				}
				return this.CurrentPageIndex * this.PageSize;
			}
		}

		// Token: 0x17002A0A RID: 10762
		// (get) Token: 0x060084FC RID: 34044 RVA: 0x001E61E1 File Offset: 0x001E43E1
		public bool IsFirstPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == 0;
			}
		}

		// Token: 0x17002A0B RID: 10763
		// (get) Token: 0x060084FD RID: 34045 RVA: 0x001E61F6 File Offset: 0x001E43F6
		public bool IsLastPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == this.PageCount - 1;
			}
		}

		// Token: 0x17002A0C RID: 10764
		// (get) Token: 0x060084FE RID: 34046 RVA: 0x001E6212 File Offset: 0x001E4412
		public int LastIndexInPage
		{
			get
			{
				if (this.IsPagingEnabled)
				{
					return Math.Min(this.DataSourceCount - 1, this.FirstIndexInPage + this.PageSize - 1);
				}
				return this.DataSourceCount - 1;
			}
		}

		// Token: 0x17002A0D RID: 10765
		// (get) Token: 0x060084FF RID: 34047 RVA: 0x001E6241 File Offset: 0x001E4441
		public int PageCount
		{
			get
			{
				if (this.IsPagingEnabled && this.DataSourceCount > 0)
				{
					return (this.DataSourceCount + this.PageSize - 1) / this.PageSize;
				}
				return 1;
			}
		}

		// Token: 0x17002A0E RID: 10766
		// (get) Token: 0x06008500 RID: 34048 RVA: 0x001E626C File Offset: 0x001E446C
		public bool IsPagingEnabled
		{
			get
			{
				return this.AllowPaging && this.PageSize != 0;
			}
		}

		// Token: 0x04002513 RID: 9491
		private int _dataSourceCount;
	}
}
