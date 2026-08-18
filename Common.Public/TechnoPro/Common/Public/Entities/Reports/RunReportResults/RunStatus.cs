using System;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x0200023A RID: 570
	public class RunStatus
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0001824F File Offset: 0x0001644F
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00018257 File Offset: 0x00016457
		public string ErrorMessage { get; set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00018260 File Offset: 0x00016460
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00018268 File Offset: 0x00016468
		public eRunStatusStep LastStatusStep { get; set; }
	}
}
