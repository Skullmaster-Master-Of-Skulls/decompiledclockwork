using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001275 RID: 4725
	public class TreeListPagingManager
	{
		// Token: 0x0600C4E7 RID: 50407 RVA: 0x002C02C0 File Offset: 0x002BE4C0
		public TreeListPagingManager(TreeListEnumerableBase enumerableBase)
		{
			this._enumerable = enumerableBase;
		}

		// Token: 0x17003F7E RID: 16254
		// (get) Token: 0x0600C4E8 RID: 50408 RVA: 0x002C02CF File Offset: 0x002BE4CF
		public int DataSourceCount
		{
			get
			{
				return this._enumerable.DataSourceCount;
			}
		}

		// Token: 0x17003F7F RID: 16255
		// (get) Token: 0x0600C4E9 RID: 50409 RVA: 0x002C02DC File Offset: 0x002BE4DC
		public int Count
		{
			get
			{
				return this._enumerable.Count;
			}
		}

		// Token: 0x17003F80 RID: 16256
		// (get) Token: 0x0600C4EA RID: 50410 RVA: 0x002C02E9 File Offset: 0x002BE4E9
		// (set) Token: 0x0600C4EB RID: 50411 RVA: 0x002C02F1 File Offset: 0x002BE4F1
		public int CurrentPageIndex { get; internal set; }

		// Token: 0x17003F81 RID: 16257
		// (get) Token: 0x0600C4EC RID: 50412 RVA: 0x002C02FA File Offset: 0x002BE4FA
		// (set) Token: 0x0600C4ED RID: 50413 RVA: 0x002C0302 File Offset: 0x002BE502
		public int PageSize { get; internal set; }

		// Token: 0x17003F82 RID: 16258
		// (get) Token: 0x0600C4EE RID: 50414 RVA: 0x002C030B File Offset: 0x002BE50B
		// (set) Token: 0x0600C4EF RID: 50415 RVA: 0x002C0313 File Offset: 0x002BE513
		public bool AllowPaging { get; internal set; }

		// Token: 0x17003F83 RID: 16259
		// (get) Token: 0x0600C4F0 RID: 50416 RVA: 0x002C031C File Offset: 0x002BE51C
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

		// Token: 0x17003F84 RID: 16260
		// (get) Token: 0x0600C4F1 RID: 50417 RVA: 0x002C0335 File Offset: 0x002BE535
		public bool IsFirstPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == 0;
			}
		}

		// Token: 0x17003F85 RID: 16261
		// (get) Token: 0x0600C4F2 RID: 50418 RVA: 0x002C034A File Offset: 0x002BE54A
		public bool IsLastPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == this.PageCount - 1;
			}
		}

		// Token: 0x17003F86 RID: 16262
		// (get) Token: 0x0600C4F3 RID: 50419 RVA: 0x002C0366 File Offset: 0x002BE566
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

		// Token: 0x17003F87 RID: 16263
		// (get) Token: 0x0600C4F4 RID: 50420 RVA: 0x002C0398 File Offset: 0x002BE598
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

		// Token: 0x17003F88 RID: 16264
		// (get) Token: 0x0600C4F5 RID: 50421 RVA: 0x002C03D9 File Offset: 0x002BE5D9
		public bool IsPagingEnabled
		{
			get
			{
				return this._enumerable.SupportsPaging && this.AllowPaging && this.PageSize != 0;
			}
		}

		// Token: 0x04003416 RID: 13334
		private readonly TreeListEnumerableBase _enumerable;
	}
}
