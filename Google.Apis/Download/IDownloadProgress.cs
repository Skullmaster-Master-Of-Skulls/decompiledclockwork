using System;

namespace Google.Apis.Download
{
	// Token: 0x0200000C RID: 12
	public interface IDownloadProgress
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000056 RID: 86
		DownloadStatus Status { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87
		long BytesDownloaded { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88
		Exception Exception { get; }
	}
}
