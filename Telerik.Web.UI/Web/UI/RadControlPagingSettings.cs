using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001C8 RID: 456
	public class RadControlPagingSettings
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
		public RadControlPagingSettings()
		{
			this.PageSize = 10;
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0003D104 File Offset: 0x0003B304
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x0003D10C File Offset: 0x0003B30C
		public int CurrentPageIndex { get; set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0003D115 File Offset: 0x0003B315
		public int PageCount
		{
			get
			{
				if (this.DataSourceCount != 0)
				{
					return (this.DataSourceCount + this.PageSize - 1) / this.PageSize;
				}
				return 1;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0003D137 File Offset: 0x0003B337
		public int FirstIndexInPage
		{
			get
			{
				return this.CurrentPageIndex * this.PageSize;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0003D146 File Offset: 0x0003B346
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0003D14E File Offset: 0x0003B34E
		public int PageSize { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0003D157 File Offset: 0x0003B357
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0003D15F File Offset: 0x0003B35F
		public int? CustomPageSize { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0003D168 File Offset: 0x0003B368
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x0003D170 File Offset: 0x0003B370
		public int DataSourceCount { get; set; }
	}
}
