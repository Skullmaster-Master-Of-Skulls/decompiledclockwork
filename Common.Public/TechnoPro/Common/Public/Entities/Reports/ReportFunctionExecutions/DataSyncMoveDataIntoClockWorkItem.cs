using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000247 RID: 583
	public class DataSyncMoveDataIntoClockWorkItem
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00018500 File Offset: 0x00016700
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00018508 File Offset: 0x00016708
		public string FullPathAndFilename { get; set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00018511 File Offset: 0x00016711
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00018519 File Offset: 0x00016719
		public string CustomTableNameWithoutCustomPrefix { get; set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00018522 File Offset: 0x00016722
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x0001852A File Offset: 0x0001672A
		public string StudentNumberExternalColumnName { get; set; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00018533 File Offset: 0x00016733
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0001853B File Offset: 0x0001673B
		public DataSyncMoveDataIntoClockWorkSourceFileInfo OverrideSourceFileInfo { get; set; }
	}
}
