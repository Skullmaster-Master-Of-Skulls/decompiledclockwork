using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000048 RID: 72
	[Flags]
	internal enum DllCharacteristics : ushort
	{
		// Token: 0x04000265 RID: 613
		ProcessInit = 1,
		// Token: 0x04000266 RID: 614
		ProcessTerm = 2,
		// Token: 0x04000267 RID: 615
		ThreadInit = 4,
		// Token: 0x04000268 RID: 616
		ThreadTerm = 8,
		// Token: 0x04000269 RID: 617
		HighEntropyVirtualAddressSpace = 32,
		// Token: 0x0400026A RID: 618
		DynamicBase = 64,
		// Token: 0x0400026B RID: 619
		NxCompatible = 256,
		// Token: 0x0400026C RID: 620
		NoIsolation = 512,
		// Token: 0x0400026D RID: 621
		NoSeh = 1024,
		// Token: 0x0400026E RID: 622
		NoBind = 2048,
		// Token: 0x0400026F RID: 623
		AppContainer = 4096,
		// Token: 0x04000270 RID: 624
		WdmDriver = 8192,
		// Token: 0x04000271 RID: 625
		TerminalServerAware = 32768
	}
}
