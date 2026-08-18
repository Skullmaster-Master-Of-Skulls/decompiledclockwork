using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000063 RID: 99
	[Flags]
	public enum RecyclingLogEventOnRecycle
	{
		// Token: 0x040000F1 RID: 241
		None = 0,
		// Token: 0x040000F2 RID: 242
		Time = 1,
		// Token: 0x040000F3 RID: 243
		Requests = 2,
		// Token: 0x040000F4 RID: 244
		Schedule = 4,
		// Token: 0x040000F5 RID: 245
		Memory = 8,
		// Token: 0x040000F6 RID: 246
		IsapiUnhealthy = 16,
		// Token: 0x040000F7 RID: 247
		OnDemand = 32,
		// Token: 0x040000F8 RID: 248
		ConfigChange = 64,
		// Token: 0x040000F9 RID: 249
		PrivateMemory = 128
	}
}
