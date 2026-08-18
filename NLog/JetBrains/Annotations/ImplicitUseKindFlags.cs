using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum ImplicitUseKindFlags
	{
		// Token: 0x0400000C RID: 12
		Default = 7,
		// Token: 0x0400000D RID: 13
		Access = 1,
		// Token: 0x0400000E RID: 14
		Assign = 2,
		// Token: 0x0400000F RID: 15
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000010 RID: 16
		InstantiatedNoFixedConstructorSignature = 8
	}
}
