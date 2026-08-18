using System;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000A8 RID: 168
	[Flags]
	public enum MemoryMappedFileRights
	{
		// Token: 0x0400052C RID: 1324
		CopyOnWrite = 1,
		// Token: 0x0400052D RID: 1325
		Write = 2,
		// Token: 0x0400052E RID: 1326
		Read = 4,
		// Token: 0x0400052F RID: 1327
		Execute = 8,
		// Token: 0x04000530 RID: 1328
		Delete = 65536,
		// Token: 0x04000531 RID: 1329
		ReadPermissions = 131072,
		// Token: 0x04000532 RID: 1330
		ChangePermissions = 262144,
		// Token: 0x04000533 RID: 1331
		TakeOwnership = 524288,
		// Token: 0x04000534 RID: 1332
		ReadWrite = 6,
		// Token: 0x04000535 RID: 1333
		ReadExecute = 12,
		// Token: 0x04000536 RID: 1334
		ReadWriteExecute = 14,
		// Token: 0x04000537 RID: 1335
		FullControl = 983055,
		// Token: 0x04000538 RID: 1336
		AccessSystemSecurity = 16777216
	}
}
