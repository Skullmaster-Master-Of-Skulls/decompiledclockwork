using System;
using Telerik.Web.UI.CloudUpload;

namespace Telerik.Web.UI
{
	// Token: 0x0200012A RID: 298
	public class CloudUploadFileInfo
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x0002D60F File Offset: 0x0002B80F
		internal CloudUploadFileInfo(UploadedFileRecord record)
		{
			this.OriginalFileName = record.OriginalFileName;
			this.KeyName = record.KeyName;
			this.ContentType = record.ContentType;
			this.ContentLength = record.ContentLength;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002D647 File Offset: 0x0002B847
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0002D64F File Offset: 0x0002B84F
		public string OriginalFileName { get; internal set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002D658 File Offset: 0x0002B858
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0002D660 File Offset: 0x0002B860
		public string KeyName { get; internal set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002D669 File Offset: 0x0002B869
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0002D671 File Offset: 0x0002B871
		public string ContentType { get; internal set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002D67A File Offset: 0x0002B87A
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0002D682 File Offset: 0x0002B882
		public long ContentLength { get; internal set; }
	}
}
