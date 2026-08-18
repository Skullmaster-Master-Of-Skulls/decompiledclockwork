using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000578 RID: 1400
	public class StudentMediaContentFileTrackingInfo : MediaContentFileWithoutData
	{
		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x00032086 File Offset: 0x00030286
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x0003208E File Offset: 0x0003028E
		public int StudentMediaContentFileId { get; set; }

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x00032097 File Offset: 0x00030297
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x0003209F File Offset: 0x0003029F
		public int StudentPersonId { get; set; }

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000320A8 File Offset: 0x000302A8
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x000320B0 File Offset: 0x000302B0
		public DateTime FileDownloadTime { get; set; }
	}
}
