using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200021B RID: 539
	public class ColumnSortingRule
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000177E0 File Offset: 0x000159E0
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x000177E8 File Offset: 0x000159E8
		public string ColumnName { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000177F1 File Offset: 0x000159F1
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000177F9 File Offset: 0x000159F9
		public bool SortDescending { get; set; }
	}
}
