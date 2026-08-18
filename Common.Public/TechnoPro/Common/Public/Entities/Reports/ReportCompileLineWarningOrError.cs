using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000214 RID: 532
	public class ReportCompileLineWarningOrError
	{
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000175EB File Offset: 0x000157EB
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x000175F3 File Offset: 0x000157F3
		public eReportCompileLineWarningOrErrorType LineType { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000175FC File Offset: 0x000157FC
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x00017604 File Offset: 0x00015804
		public string Message { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0001760D File Offset: 0x0001580D
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x00017615 File Offset: 0x00015815
		public int LineNumber { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0001761E File Offset: 0x0001581E
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x00017626 File Offset: 0x00015826
		public int ColumnNumber { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0001762F File Offset: 0x0001582F
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x00017637 File Offset: 0x00015837
		public string Filename { get; set; }
	}
}
