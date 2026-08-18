using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200062F RID: 1583
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostProtectionResource
	{
		// Token: 0x04001DA3 RID: 7587
		None = 0,
		// Token: 0x04001DA4 RID: 7588
		Synchronization = 1,
		// Token: 0x04001DA5 RID: 7589
		SharedState = 2,
		// Token: 0x04001DA6 RID: 7590
		ExternalProcessMgmt = 4,
		// Token: 0x04001DA7 RID: 7591
		SelfAffectingProcessMgmt = 8,
		// Token: 0x04001DA8 RID: 7592
		ExternalThreading = 16,
		// Token: 0x04001DA9 RID: 7593
		SelfAffectingThreading = 32,
		// Token: 0x04001DAA RID: 7594
		SecurityInfrastructure = 64,
		// Token: 0x04001DAB RID: 7595
		UI = 128,
		// Token: 0x04001DAC RID: 7596
		MayLeakOnAbort = 256,
		// Token: 0x04001DAD RID: 7597
		All = 511
	}
}
