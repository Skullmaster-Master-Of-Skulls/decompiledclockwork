using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000024 RID: 36
	[Flags]
	public enum DllCharacteristics : ushort
	{
		// Token: 0x04000102 RID: 258
		ProcessInit = 1,
		// Token: 0x04000103 RID: 259
		ProcessTerm = 2,
		// Token: 0x04000104 RID: 260
		ThreadInit = 4,
		// Token: 0x04000105 RID: 261
		ThreadTerm = 8,
		// Token: 0x04000106 RID: 262
		HighEntropyVirtualAddressSpace = 32,
		// Token: 0x04000107 RID: 263
		DynamicBase = 64,
		// Token: 0x04000108 RID: 264
		NxCompatible = 256,
		// Token: 0x04000109 RID: 265
		NoIsolation = 512,
		// Token: 0x0400010A RID: 266
		NoSeh = 1024,
		// Token: 0x0400010B RID: 267
		NoBind = 2048,
		// Token: 0x0400010C RID: 268
		AppContainer = 4096,
		// Token: 0x0400010D RID: 269
		WdmDriver = 8192,
		// Token: 0x0400010E RID: 270
		TerminalServerAware = 32768
	}
}
