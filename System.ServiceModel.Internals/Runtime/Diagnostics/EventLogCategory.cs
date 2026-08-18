using System;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000047 RID: 71
	internal enum EventLogCategory : ushort
	{
		// Token: 0x04000132 RID: 306
		ServiceAuthorization = 1,
		// Token: 0x04000133 RID: 307
		MessageAuthentication,
		// Token: 0x04000134 RID: 308
		ObjectAccess,
		// Token: 0x04000135 RID: 309
		Tracing,
		// Token: 0x04000136 RID: 310
		WebHost,
		// Token: 0x04000137 RID: 311
		FailFast,
		// Token: 0x04000138 RID: 312
		MessageLogging,
		// Token: 0x04000139 RID: 313
		PerformanceCounter,
		// Token: 0x0400013A RID: 314
		Wmi,
		// Token: 0x0400013B RID: 315
		ComPlus,
		// Token: 0x0400013C RID: 316
		StateMachine,
		// Token: 0x0400013D RID: 317
		Wsat,
		// Token: 0x0400013E RID: 318
		SharingService,
		// Token: 0x0400013F RID: 319
		ListenerAdapter
	}
}
