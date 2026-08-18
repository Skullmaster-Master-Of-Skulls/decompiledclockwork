using System;

namespace TechnoPro.Common.Public.Entities.Legacy.DynamicData
{
	// Token: 0x020002FB RID: 763
	public class LegacyDynamicDataItemItemsToBeDecrypted : BusinessBase<int>
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x0001C4AC File Offset: 0x0001A6AC
		public byte[] ControlValueBytes { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0001C4B5 File Offset: 0x0001A6B5
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x0001C4BD File Offset: 0x0001A6BD
		public byte[] TextForLetterEncrypted { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0001C4C6 File Offset: 0x0001A6C6
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x0001C4CE File Offset: 0x0001A6CE
		public byte[] PrivateNoteEncrypted { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0001C4D7 File Offset: 0x0001A6D7
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x0001C4DF File Offset: 0x0001A6DF
		public byte[] RecommendedToStudentButDeclinedDetailEncrypted { get; set; }
	}
}
