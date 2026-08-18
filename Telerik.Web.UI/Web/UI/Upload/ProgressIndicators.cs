using System;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B7D RID: 7037
	[Flags]
	public enum ProgressIndicators
	{
		// Token: 0x04004C46 RID: 19526
		None = 0,
		// Token: 0x04004C47 RID: 19527
		TotalProgressBar = 1,
		// Token: 0x04004C48 RID: 19528
		TotalProgress = 2,
		// Token: 0x04004C49 RID: 19529
		TotalProgressPercent = 4,
		// Token: 0x04004C4A RID: 19530
		RequestSize = 8,
		// Token: 0x04004C4B RID: 19531
		FilesCountBar = 16,
		// Token: 0x04004C4C RID: 19532
		FilesCount = 32,
		// Token: 0x04004C4D RID: 19533
		FilesCountPercent = 64,
		// Token: 0x04004C4E RID: 19534
		SelectedFilesCount = 128,
		// Token: 0x04004C4F RID: 19535
		CurrentFileName = 256,
		// Token: 0x04004C50 RID: 19536
		TimeElapsed = 512,
		// Token: 0x04004C51 RID: 19537
		TimeEstimated = 1024,
		// Token: 0x04004C52 RID: 19538
		TransferSpeed = 2048
	}
}
