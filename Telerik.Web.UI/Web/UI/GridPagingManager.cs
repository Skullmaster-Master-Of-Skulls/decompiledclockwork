using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200115B RID: 4443
	public class GridPagingManager
	{
		// Token: 0x0600B51E RID: 46366 RVA: 0x0027ECAA File Offset: 0x0027CEAA
		internal GridPagingManager(GridEnumerableBase enumerable)
		{
			this.enumerable = enumerable;
		}

		// Token: 0x17003A7F RID: 14975
		// (get) Token: 0x0600B51F RID: 46367 RVA: 0x0027ECB9 File Offset: 0x0027CEB9
		public bool AllowPaging
		{
			get
			{
				return this.enumerable.SupportsPaging && this._allowPaging;
			}
		}

		// Token: 0x0600B520 RID: 46368 RVA: 0x0027ECD0 File Offset: 0x0027CED0
		internal void setAllowPaging(bool value)
		{
			this._allowPaging = value;
		}

		// Token: 0x17003A80 RID: 14976
		// (get) Token: 0x0600B521 RID: 46369 RVA: 0x0027ECD9 File Offset: 0x0027CED9
		public int CurrentPageIndex
		{
			get
			{
				return this._currentPageIndex;
			}
		}

		// Token: 0x0600B522 RID: 46370 RVA: 0x0027ECE1 File Offset: 0x0027CEE1
		internal void setCurrentPageIndex(int value)
		{
			this._currentPageIndex = ((this.PageSize == int.MaxValue) ? 0 : value);
		}

		// Token: 0x17003A81 RID: 14977
		// (get) Token: 0x0600B523 RID: 46371 RVA: 0x0027ECFC File Offset: 0x0027CEFC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public int PageCount
		{
			get
			{
				if (!this.enumerable.SupportsPaging)
				{
					return 1;
				}
				int dataSourceCount = this.DataSourceCount;
				if (this.IsPagingEnabled && dataSourceCount != 0)
				{
					return (int)(((long)dataSourceCount + (long)this._pageSize - 1L) / (long)this._pageSize);
				}
				return 1;
			}
		}

		// Token: 0x17003A82 RID: 14978
		// (get) Token: 0x0600B524 RID: 46372 RVA: 0x0027ED42 File Offset: 0x0027CF42
		public bool IsPagingEnabled
		{
			get
			{
				return this.enumerable.SupportsPaging && this._allowPaging && this._pageSize != 0;
			}
		}

		// Token: 0x17003A83 RID: 14979
		// (get) Token: 0x0600B525 RID: 46373 RVA: 0x0027ED69 File Offset: 0x0027CF69
		public int PageSize
		{
			get
			{
				return this._pageSize;
			}
		}

		// Token: 0x0600B526 RID: 46374 RVA: 0x0027ED71 File Offset: 0x0027CF71
		internal void setPageSize(int value)
		{
			this._pageSize = value;
		}

		// Token: 0x17003A84 RID: 14980
		// (get) Token: 0x0600B527 RID: 46375 RVA: 0x0027ED7A File Offset: 0x0027CF7A
		public int FirstIndexInPage
		{
			get
			{
				if (!this.IsPagingEnabled)
				{
					return 0;
				}
				return this._currentPageIndex * this._pageSize;
			}
		}

		// Token: 0x17003A85 RID: 14981
		// (get) Token: 0x0600B528 RID: 46376 RVA: 0x0027ED93 File Offset: 0x0027CF93
		public bool IsCustomPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this._allowCustomPaging;
			}
		}

		// Token: 0x17003A86 RID: 14982
		// (get) Token: 0x0600B529 RID: 46377 RVA: 0x0027EDA5 File Offset: 0x0027CFA5
		public bool AllowCustomPaging
		{
			get
			{
				return this._allowCustomPaging;
			}
		}

		// Token: 0x0600B52A RID: 46378 RVA: 0x0027EDAD File Offset: 0x0027CFAD
		internal void setAllowCustomPaging(bool value)
		{
			this._allowCustomPaging = value;
		}

		// Token: 0x17003A87 RID: 14983
		// (get) Token: 0x0600B52B RID: 46379 RVA: 0x0027EDB6 File Offset: 0x0027CFB6
		public bool IsFirstPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == 0;
			}
		}

		// Token: 0x17003A88 RID: 14984
		// (get) Token: 0x0600B52C RID: 46380 RVA: 0x0027EDCB File Offset: 0x0027CFCB
		public bool IsLastPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == this.PageCount - 1;
			}
		}

		// Token: 0x17003A89 RID: 14985
		// (get) Token: 0x0600B52D RID: 46381 RVA: 0x0027EDE7 File Offset: 0x0027CFE7
		public int VirtualCount
		{
			get
			{
				return this._virtualCount;
			}
		}

		// Token: 0x0600B52E RID: 46382 RVA: 0x0027EDEF File Offset: 0x0027CFEF
		internal void setVirtualCount(int value)
		{
			this._virtualCount = value;
		}

		// Token: 0x17003A8A RID: 14986
		// (get) Token: 0x0600B52F RID: 46383 RVA: 0x0027EDF8 File Offset: 0x0027CFF8
		public int LastIndexInPage
		{
			get
			{
				int result;
				if (this.IsPagingEnabled)
				{
					if (!this.IsCustomPagingEnabled)
					{
						result = Math.Min(this.DataSourceCount - 1, this.FirstIndexInPage + this.PageSize - 1);
					}
					else
					{
						result = Math.Min(this.DataSourceCount - 1, this._currentPageIndex * this._pageSize + this._pageSize - 1);
					}
				}
				else
				{
					result = this.DataSourceCount - 1;
				}
				return result;
			}
		}

		// Token: 0x17003A8B RID: 14987
		// (get) Token: 0x0600B530 RID: 46384 RVA: 0x0027EE64 File Offset: 0x0027D064
		public int DataSourceCount
		{
			get
			{
				if (this.IsCustomPagingEnabled)
				{
					return this.VirtualCount;
				}
				return this.enumerable.DataSourceCount;
			}
		}

		// Token: 0x17003A8C RID: 14988
		// (get) Token: 0x0600B531 RID: 46385 RVA: 0x0027EE80 File Offset: 0x0027D080
		public int Count
		{
			get
			{
				if (!this.IsPagingEnabled)
				{
					return this.DataSourceCount;
				}
				if (!this.IsCustomPagingEnabled && this.IsLastPage)
				{
					return this.DataSourceCount - this.FirstIndexInPage;
				}
				return this._pageSize;
			}
		}

		// Token: 0x04002FBD RID: 12221
		private GridEnumerableBase enumerable;

		// Token: 0x04002FBE RID: 12222
		private bool _allowPaging;

		// Token: 0x04002FBF RID: 12223
		private int _currentPageIndex;

		// Token: 0x04002FC0 RID: 12224
		private int _pageSize;

		// Token: 0x04002FC1 RID: 12225
		private bool _allowCustomPaging;

		// Token: 0x04002FC2 RID: 12226
		private int _virtualCount;
	}
}
