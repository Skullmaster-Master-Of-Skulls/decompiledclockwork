using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000152 RID: 338
	public class UploadUpdateFileResult
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000115E5 File Offset: 0x0000F7E5
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x000115ED File Offset: 0x0000F7ED
		public string Filename { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000115F6 File Offset: 0x0000F7F6
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x000115FE File Offset: 0x0000F7FE
		public string Folder { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00011607 File Offset: 0x0000F807
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001160F File Offset: 0x0000F80F
		public bool WasSuccessfullUpload { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00011618 File Offset: 0x0000F818
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00011620 File Offset: 0x0000F820
		public string ErrorMessage { get; set; }
	}
}
