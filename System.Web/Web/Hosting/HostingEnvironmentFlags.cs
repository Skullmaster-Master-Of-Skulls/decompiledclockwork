using System;

namespace System.Web.Hosting
{
	// Token: 0x02000289 RID: 649
	[Flags]
	internal enum HostingEnvironmentFlags
	{
		// Token: 0x04001B01 RID: 6913
		Default = 0,
		// Token: 0x04001B02 RID: 6914
		HideFromAppManager = 1,
		// Token: 0x04001B03 RID: 6915
		ThrowHostingInitErrors = 2,
		// Token: 0x04001B04 RID: 6916
		DontCallAppInitialize = 4,
		// Token: 0x04001B05 RID: 6917
		ClientBuildManager = 8
	}
}
