using System;

namespace System.Reflection
{
	// Token: 0x0200000F RID: 15
	[Flags]
	public enum AssemblyFlags
	{
		// Token: 0x04000047 RID: 71
		PublicKey = 1,
		// Token: 0x04000048 RID: 72
		Retargetable = 256,
		// Token: 0x04000049 RID: 73
		WindowsRuntime = 512,
		// Token: 0x0400004A RID: 74
		ContentTypeMask = 3584,
		// Token: 0x0400004B RID: 75
		DisableJitCompileOptimizer = 16384,
		// Token: 0x0400004C RID: 76
		EnableJitCompileTracking = 32768
	}
}
