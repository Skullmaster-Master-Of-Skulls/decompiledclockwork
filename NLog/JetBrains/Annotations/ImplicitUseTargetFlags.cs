using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000E RID: 14
	[Flags]
	internal enum ImplicitUseTargetFlags
	{
		// Token: 0x04000012 RID: 18
		Default = 1,
		// Token: 0x04000013 RID: 19
		Itself = 1,
		// Token: 0x04000014 RID: 20
		Members = 2,
		// Token: 0x04000015 RID: 21
		WithMembers = 3
	}
}
