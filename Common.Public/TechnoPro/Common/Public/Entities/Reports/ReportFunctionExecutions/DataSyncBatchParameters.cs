using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200023E RID: 574
	public class DataSyncBatchParameters
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x000182E0 File Offset: 0x000164E0
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x000182E8 File Offset: 0x000164E8
		public int OverrideImportStudentDataReportId { get; set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x000182F1 File Offset: 0x000164F1
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x000182F9 File Offset: 0x000164F9
		public int OverrideImportStudentCoursesReportId { get; set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00018302 File Offset: 0x00016502
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0001830A File Offset: 0x0001650A
		public bool UseSingleThread { get; set; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00018313 File Offset: 0x00016513
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0001831B File Offset: 0x0001651B
		public TimeSpan AllowedTimeToRun { get; set; }

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00018324 File Offset: 0x00016524
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0001832C File Offset: 0x0001652C
		public int LastDataSyncControlId { get; set; }
	}
}
