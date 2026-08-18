using System;

namespace Google.Apis.Http
{
	// Token: 0x02000027 RID: 39
	[Flags]
	public enum ExponentialBackOffPolicy
	{
		// Token: 0x04000056 RID: 86
		None = 0,
		// Token: 0x04000057 RID: 87
		Exception = 1,
		// Token: 0x04000058 RID: 88
		UnsuccessfulResponse503 = 2
	}
}
