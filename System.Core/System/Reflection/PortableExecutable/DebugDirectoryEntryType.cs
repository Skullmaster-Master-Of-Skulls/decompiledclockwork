using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000041 RID: 65
	internal enum DebugDirectoryEntryType
	{
		// Token: 0x0400021F RID: 543
		Unknown,
		// Token: 0x04000220 RID: 544
		Coff,
		// Token: 0x04000221 RID: 545
		CodeView,
		// Token: 0x04000222 RID: 546
		Reproducible = 16,
		// Token: 0x04000223 RID: 547
		EmbeddedPortablePdb
	}
}
