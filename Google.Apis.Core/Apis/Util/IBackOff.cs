using System;

namespace Google.Apis.Util
{
	// Token: 0x02000006 RID: 6
	public interface IBackOff
	{
		// Token: 0x06000016 RID: 22
		TimeSpan GetNextBackOff(int currentRetry);

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23
		int MaxNumOfRetries { get; }
	}
}
