using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000249 RID: 585
	public class DataSyncMoveDataIntoClockWorkSourceFileInfo
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00018544 File Offset: 0x00016744
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x0001854C File Offset: 0x0001674C
		public eDataSyncMoveDataIntoClockWorkSourceFileType SourceFileType { get; set; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00018555 File Offset: 0x00016755
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x0001855D File Offset: 0x0001675D
		public string[] Args { get; set; }
	}
}
