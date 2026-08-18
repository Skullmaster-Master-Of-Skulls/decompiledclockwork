using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000107 RID: 263
	[Flags]
	public enum CngKeyCreationOptions
	{
		// Token: 0x0400068A RID: 1674
		None = 0,
		// Token: 0x0400068B RID: 1675
		MachineKey = 32,
		// Token: 0x0400068C RID: 1676
		OverwriteExistingKey = 128
	}
}
