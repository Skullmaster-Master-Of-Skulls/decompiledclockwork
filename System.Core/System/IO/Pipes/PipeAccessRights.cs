using System;

namespace System.IO.Pipes
{
	// Token: 0x020000B8 RID: 184
	[Flags]
	public enum PipeAccessRights
	{
		// Token: 0x04000584 RID: 1412
		ReadData = 1,
		// Token: 0x04000585 RID: 1413
		WriteData = 2,
		// Token: 0x04000586 RID: 1414
		ReadAttributes = 128,
		// Token: 0x04000587 RID: 1415
		WriteAttributes = 256,
		// Token: 0x04000588 RID: 1416
		ReadExtendedAttributes = 8,
		// Token: 0x04000589 RID: 1417
		WriteExtendedAttributes = 16,
		// Token: 0x0400058A RID: 1418
		CreateNewInstance = 4,
		// Token: 0x0400058B RID: 1419
		Delete = 65536,
		// Token: 0x0400058C RID: 1420
		ReadPermissions = 131072,
		// Token: 0x0400058D RID: 1421
		ChangePermissions = 262144,
		// Token: 0x0400058E RID: 1422
		TakeOwnership = 524288,
		// Token: 0x0400058F RID: 1423
		Synchronize = 1048576,
		// Token: 0x04000590 RID: 1424
		FullControl = 2032031,
		// Token: 0x04000591 RID: 1425
		Read = 131209,
		// Token: 0x04000592 RID: 1426
		Write = 274,
		// Token: 0x04000593 RID: 1427
		ReadWrite = 131483,
		// Token: 0x04000594 RID: 1428
		AccessSystemSecurity = 16777216
	}
}
