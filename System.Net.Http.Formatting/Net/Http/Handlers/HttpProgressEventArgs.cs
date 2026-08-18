using System;
using System.ComponentModel;

namespace System.Net.Http.Handlers
{
	// Token: 0x02000026 RID: 38
	public class HttpProgressEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000056AA File Offset: 0x000038AA
		public HttpProgressEventArgs(int progressPercentage, object userToken, long bytesTransferred, long? totalBytes) : base(progressPercentage, userToken)
		{
			this.BytesTransferred = bytesTransferred;
			this.TotalBytes = totalBytes;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000056C3 File Offset: 0x000038C3
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000056CB File Offset: 0x000038CB
		public long BytesTransferred { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000056D4 File Offset: 0x000038D4
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000056DC File Offset: 0x000038DC
		public long? TotalBytes { get; private set; }
	}
}
