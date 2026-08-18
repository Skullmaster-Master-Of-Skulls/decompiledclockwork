using System;

namespace System.Security.Policy
{
	// Token: 0x02000105 RID: 261
	[Flags]
	internal enum TrustManagerPromptOptions
	{
		// Token: 0x04000458 RID: 1112
		None = 0,
		// Token: 0x04000459 RID: 1113
		StopApp = 1,
		// Token: 0x0400045A RID: 1114
		RequiresPermissions = 2,
		// Token: 0x0400045B RID: 1115
		WillHaveFullTrust = 4,
		// Token: 0x0400045C RID: 1116
		AddsShortcut = 8,
		// Token: 0x0400045D RID: 1117
		LocalNetworkSource = 16,
		// Token: 0x0400045E RID: 1118
		LocalComputerSource = 32,
		// Token: 0x0400045F RID: 1119
		InternetSource = 64,
		// Token: 0x04000460 RID: 1120
		TrustedSitesSource = 128,
		// Token: 0x04000461 RID: 1121
		UntrustedSitesSource = 256
	}
}
