using System;
using System.ComponentModel;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x020016AE RID: 5806
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class RadAsyncUploadClientState : RadUploadClientSide
	{
		// Token: 0x170044B5 RID: 17589
		// (get) Token: 0x0600E039 RID: 57401 RVA: 0x0031DE8F File Offset: 0x0031C08F
		// (set) Token: 0x0600E03A RID: 57402 RVA: 0x0031DE97 File Offset: 0x0031C097
		public UploadedFileInfo[] UploadedFiles { get; set; }
	}
}
