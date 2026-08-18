using System;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B73 RID: 7027
	public class UploadedFileEventArgs : EventArgs
	{
		// Token: 0x17005322 RID: 21282
		// (get) Token: 0x0601107C RID: 69756 RVA: 0x003C279D File Offset: 0x003C099D
		public UploadedFile UploadedFile
		{
			get
			{
				return this._uploadedFile;
			}
		}

		// Token: 0x0601107D RID: 69757 RVA: 0x003C27A5 File Offset: 0x003C09A5
		internal UploadedFileEventArgs(UploadedFile uploadedFile)
		{
			this._uploadedFile = uploadedFile;
		}

		// Token: 0x04004C2E RID: 19502
		private UploadedFile _uploadedFile;
	}
}
