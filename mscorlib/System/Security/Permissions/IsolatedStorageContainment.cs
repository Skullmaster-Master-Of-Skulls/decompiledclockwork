using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000636 RID: 1590
	[ComVisible(true)]
	[Serializable]
	public enum IsolatedStorageContainment
	{
		// Token: 0x04001DD7 RID: 7639
		None,
		// Token: 0x04001DD8 RID: 7640
		DomainIsolationByUser = 16,
		// Token: 0x04001DD9 RID: 7641
		ApplicationIsolationByUser = 21,
		// Token: 0x04001DDA RID: 7642
		AssemblyIsolationByUser = 32,
		// Token: 0x04001DDB RID: 7643
		DomainIsolationByMachine = 48,
		// Token: 0x04001DDC RID: 7644
		AssemblyIsolationByMachine = 64,
		// Token: 0x04001DDD RID: 7645
		ApplicationIsolationByMachine = 69,
		// Token: 0x04001DDE RID: 7646
		DomainIsolationByRoamingUser = 80,
		// Token: 0x04001DDF RID: 7647
		AssemblyIsolationByRoamingUser = 96,
		// Token: 0x04001DE0 RID: 7648
		ApplicationIsolationByRoamingUser = 101,
		// Token: 0x04001DE1 RID: 7649
		AdministerIsolatedStorageByUser = 112,
		// Token: 0x04001DE2 RID: 7650
		UnrestrictedIsolatedStorage = 240
	}
}
