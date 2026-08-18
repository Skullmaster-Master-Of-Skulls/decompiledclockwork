using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000672 RID: 1650
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		// Token: 0x04001EB9 RID: 7865
		None = 0,
		// Token: 0x04001EBA RID: 7866
		HostAppDomainEvidence = 1,
		// Token: 0x04001EBB RID: 7867
		HostPolicyLevel = 2,
		// Token: 0x04001EBC RID: 7868
		HostAssemblyEvidence = 4,
		// Token: 0x04001EBD RID: 7869
		HostDetermineApplicationTrust = 8,
		// Token: 0x04001EBE RID: 7870
		HostResolvePolicy = 16,
		// Token: 0x04001EBF RID: 7871
		AllFlags = 31
	}
}
