using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200003D RID: 61
	[Flags]
	internal enum CorFlags
	{
		// Token: 0x04000201 RID: 513
		ILOnly = 1,
		// Token: 0x04000202 RID: 514
		Requires32Bit = 2,
		// Token: 0x04000203 RID: 515
		ILLibrary = 4,
		// Token: 0x04000204 RID: 516
		StrongNameSigned = 8,
		// Token: 0x04000205 RID: 517
		NativeEntryPoint = 16,
		// Token: 0x04000206 RID: 518
		TrackDebugData = 65536,
		// Token: 0x04000207 RID: 519
		Prefers32Bit = 131072
	}
}
