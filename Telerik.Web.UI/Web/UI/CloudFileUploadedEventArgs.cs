using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200012E RID: 302
	public class CloudFileUploadedEventArgs : EventArgs
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002D788 File Offset: 0x0002B988
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0002D790 File Offset: 0x0002B990
		public CloudUploadFileInfo FileInfo { get; internal set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002D799 File Offset: 0x0002B999
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0002D7A1 File Offset: 0x0002B9A1
		public bool IsValid { get; set; }
	}
}
