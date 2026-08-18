using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002BF RID: 703
	public class MailMergeCodeForExcel : MailMergeCode
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0001AB98 File Offset: 0x00018D98
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		public int RowIndex { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0001ABA9 File Offset: 0x00018DA9
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x0001ABB1 File Offset: 0x00018DB1
		public int ColIndex { get; set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0001ABBA File Offset: 0x00018DBA
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x0001ABC2 File Offset: 0x00018DC2
		public bool IsLooseMailMergeCode { get; set; }
	}
}
