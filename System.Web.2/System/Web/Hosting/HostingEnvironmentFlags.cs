using System;

namespace System.Web.Hosting
{
	// Token: 0x020007AB RID: 1963
	[Flags]
	internal enum HostingEnvironmentFlags
	{
		// Token: 0x040030FA RID: 12538
		Default = 0,
		// Token: 0x040030FB RID: 12539
		HideFromAppManager = 1,
		// Token: 0x040030FC RID: 12540
		ThrowHostingInitErrors = 2,
		// Token: 0x040030FD RID: 12541
		DontCallAppInitialize = 4,
		// Token: 0x040030FE RID: 12542
		ClientBuildManager = 8,
		// Token: 0x040030FF RID: 12543
		SupportsMultiTargeting = 16
	}
}
