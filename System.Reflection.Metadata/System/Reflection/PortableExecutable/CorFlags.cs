using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000018 RID: 24
	[Flags]
	public enum CorFlags
	{
		// Token: 0x04000092 RID: 146
		ILOnly = 1,
		// Token: 0x04000093 RID: 147
		Requires32Bit = 2,
		// Token: 0x04000094 RID: 148
		ILLibrary = 4,
		// Token: 0x04000095 RID: 149
		StrongNameSigned = 8,
		// Token: 0x04000096 RID: 150
		NativeEntryPoint = 16,
		// Token: 0x04000097 RID: 151
		TrackDebugData = 65536,
		// Token: 0x04000098 RID: 152
		Prefers32Bit = 131072
	}
}
