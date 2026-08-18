using System;

namespace Google.Apis.Upload
{
	// Token: 0x02000005 RID: 5
	public interface IUploadProgress
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		UploadStatus Status { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		long BytesSent { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		Exception Exception { get; }
	}
}
