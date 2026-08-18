using System;

namespace TechnoPro.Common.UI.Web.Entity.Common.FileUpload
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class FileForUpload
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000038AD File Offset: 0x00001AAD
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000038B5 File Offset: 0x00001AB5
		public string Filename { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000038BE File Offset: 0x00001ABE
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000038C6 File Offset: 0x00001AC6
		public int FileForUploadId { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000038CF File Offset: 0x00001ACF
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000038D7 File Offset: 0x00001AD7
		public long FileSize { get; set; }
	}
}
