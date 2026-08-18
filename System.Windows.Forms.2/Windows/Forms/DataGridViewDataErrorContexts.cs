using System;

namespace System.Windows.Forms
{
	// Token: 0x020001CE RID: 462
	[Flags]
	public enum DataGridViewDataErrorContexts
	{
		// Token: 0x04000DA8 RID: 3496
		Formatting = 1,
		// Token: 0x04000DA9 RID: 3497
		Display = 2,
		// Token: 0x04000DAA RID: 3498
		PreferredSize = 4,
		// Token: 0x04000DAB RID: 3499
		RowDeletion = 8,
		// Token: 0x04000DAC RID: 3500
		Parsing = 256,
		// Token: 0x04000DAD RID: 3501
		Commit = 512,
		// Token: 0x04000DAE RID: 3502
		InitialValueRestoration = 1024,
		// Token: 0x04000DAF RID: 3503
		LeaveControl = 2048,
		// Token: 0x04000DB0 RID: 3504
		CurrentCellChange = 4096,
		// Token: 0x04000DB1 RID: 3505
		Scroll = 8192,
		// Token: 0x04000DB2 RID: 3506
		ClipboardContent = 16384
	}
}
