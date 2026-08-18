using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000108 RID: 264
	[Flags]
	public enum CngKeyOpenOptions
	{
		// Token: 0x0400068E RID: 1678
		None = 0,
		// Token: 0x0400068F RID: 1679
		UserKey = 0,
		// Token: 0x04000690 RID: 1680
		MachineKey = 32,
		// Token: 0x04000691 RID: 1681
		Silent = 64
	}
}
