using System;

namespace System.Configuration
{
	// Token: 0x02000040 RID: 64
	[Flags]
	internal enum ConfigurationValueFlags
	{
		// Token: 0x04000221 RID: 545
		Default = 0,
		// Token: 0x04000222 RID: 546
		Inherited = 1,
		// Token: 0x04000223 RID: 547
		Modified = 2,
		// Token: 0x04000224 RID: 548
		Locked = 4,
		// Token: 0x04000225 RID: 549
		XMLParentInherited = 8
	}
}
