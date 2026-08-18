using System;

namespace TechnoPro.Common.UI.Web.Entity.Common.FileUpload
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public enum eFileUploadStatus
	{
		// Token: 0x04000138 RID: 312
		Unknown,
		// Token: 0x04000139 RID: 313
		Successful,
		// Token: 0x0400013A RID: 314
		FailedUnknown,
		// Token: 0x0400013B RID: 315
		FailedFileTooLarge,
		// Token: 0x0400013C RID: 316
		FailedInvalidExtension
	}
}
