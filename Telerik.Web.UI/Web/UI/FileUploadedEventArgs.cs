using System;
using Telerik.Web.UI.AsyncUpload;

namespace Telerik.Web.UI
{
	// Token: 0x020016AB RID: 5803
	public class FileUploadedEventArgs : EventArgs
	{
		// Token: 0x170044A8 RID: 17576
		// (get) Token: 0x0600E01B RID: 57371 RVA: 0x0031DCFC File Offset: 0x0031BEFC
		// (set) Token: 0x0600E01C RID: 57372 RVA: 0x0031DD04 File Offset: 0x0031BF04
		public UploadedFile File { get; protected set; }

		// Token: 0x170044A9 RID: 17577
		// (get) Token: 0x0600E01D RID: 57373 RVA: 0x0031DD0D File Offset: 0x0031BF0D
		// (set) Token: 0x0600E01E RID: 57374 RVA: 0x0031DD15 File Offset: 0x0031BF15
		public bool IsValid { get; set; }

		// Token: 0x170044AA RID: 17578
		// (get) Token: 0x0600E01F RID: 57375 RVA: 0x0031DD20 File Offset: 0x0031BF20
		public IAsyncUploadResult UploadResult
		{
			get
			{
				AsyncUploadedFile asyncUploadedFile = this.File as AsyncUploadedFile;
				Type type = Type.GetType(asyncUploadedFile.FileType);
				object obj = SerializationService.Deserialize(asyncUploadedFile.SerializedData, type);
				return (IAsyncUploadResult)obj;
			}
		}

		// Token: 0x0600E020 RID: 57376 RVA: 0x0031DD58 File Offset: 0x0031BF58
		public FileUploadedEventArgs(UploadedFile file, bool isValid)
		{
			this.File = file;
			this.IsValid = isValid;
		}
	}
}
