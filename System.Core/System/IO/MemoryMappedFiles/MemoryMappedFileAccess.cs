using System;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	public enum MemoryMappedFileAccess
	{
		// Token: 0x0400051C RID: 1308
		ReadWrite,
		// Token: 0x0400051D RID: 1309
		Read,
		// Token: 0x0400051E RID: 1310
		Write,
		// Token: 0x0400051F RID: 1311
		CopyOnWrite,
		// Token: 0x04000520 RID: 1312
		ReadExecute,
		// Token: 0x04000521 RID: 1313
		ReadWriteExecute
	}
}
