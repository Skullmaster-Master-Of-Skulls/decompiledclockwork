using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000919 RID: 2329
	[Flags]
	public enum PropagationFlags
	{
		// Token: 0x04002BA0 RID: 11168
		None = 0,
		// Token: 0x04002BA1 RID: 11169
		NoPropagateInherit = 1,
		// Token: 0x04002BA2 RID: 11170
		InheritOnly = 2
	}
}
