using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200094D RID: 2381
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04002CE7 RID: 11495
		None = 0,
		// Token: 0x04002CE8 RID: 11496
		AppDomainID = 1,
		// Token: 0x04002CE9 RID: 11497
		ProcessID = 2,
		// Token: 0x04002CEA RID: 11498
		AssemblyName = 4,
		// Token: 0x04002CEB RID: 11499
		TypeName = 8
	}
}
