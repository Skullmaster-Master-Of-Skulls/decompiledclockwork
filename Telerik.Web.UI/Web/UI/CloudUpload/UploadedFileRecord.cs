using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x02000129 RID: 297
	internal class UploadedFileRecord
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002D5C3 File Offset: 0x0002B7C3
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0002D5CB File Offset: 0x0002B7CB
		public string OriginalFileName { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002D5D4 File Offset: 0x0002B7D4
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0002D5DC File Offset: 0x0002B7DC
		public string KeyName { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0002D5E5 File Offset: 0x0002B7E5
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0002D5ED File Offset: 0x0002B7ED
		public string ContentType { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002D5F6 File Offset: 0x0002B7F6
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0002D5FE File Offset: 0x0002B7FE
		public long ContentLength { get; set; }
	}
}
