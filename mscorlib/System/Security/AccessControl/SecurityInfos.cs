using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200091B RID: 2331
	[Flags]
	public enum SecurityInfos
	{
		// Token: 0x04002BA8 RID: 11176
		Owner = 1,
		// Token: 0x04002BA9 RID: 11177
		Group = 2,
		// Token: 0x04002BAA RID: 11178
		DiscretionaryAcl = 4,
		// Token: 0x04002BAB RID: 11179
		SystemAcl = 8
	}
}
