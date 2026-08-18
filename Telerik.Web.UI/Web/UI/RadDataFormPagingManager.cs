using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001E7 RID: 487
	public class RadDataFormPagingManager
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x0003ED7B File Offset: 0x0003CF7B
		public RadDataFormPagingManager(DataFormEnumerableBase enumerableBase)
		{
			this._enumerable = enumerableBase;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0003ED8A File Offset: 0x0003CF8A
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003EDA6 File Offset: 0x0003CFA6
		public int Count
		{
			get
			{
				return this._enumerable.Count;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0003EDB3 File Offset: 0x0003CFB3
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x0003EDBB File Offset: 0x0003CFBB
		public int CurrentPageIndex { get; internal set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0003EDC4 File Offset: 0x0003CFC4
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x0003EDCC File Offset: 0x0003CFCC
		public int PageSize { get; internal set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0003EDD5 File Offset: 0x0003CFD5
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x0003EDDD File Offset: 0x0003CFDD
		public bool AllowPaging { get; internal set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0003EDE6 File Offset: 0x0003CFE6
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x0003EDEE File Offset: 0x0003CFEE
		public bool AllowCustomPaging { get; internal set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0003EDF7 File Offset: 0x0003CFF7
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x0003EDFF File Offset: 0x0003CFFF
		public int VirtualItemCount { get; internal set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0003EE08 File Offset: 0x0003D008
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0003EE49 File Offset: 0x0003D049
		public bool IsPagingEnabled
		{
			get
			{
				return this._enumerable.SupportsPaging && this.AllowPaging && this.PageSize != 0;
			}
		}

		// Token: 0x040004EE RID: 1262
		private readonly DataFormEnumerableBase _enumerable;
	}
}
