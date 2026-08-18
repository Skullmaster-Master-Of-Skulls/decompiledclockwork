using System;

namespace System.Windows.Forms
{
	// Token: 0x0200011C RID: 284
	[Flags]
	public enum AccessibleSelection
	{
		// Token: 0x040005A4 RID: 1444
		None = 0,
		// Token: 0x040005A5 RID: 1445
		TakeFocus = 1,
		// Token: 0x040005A6 RID: 1446
		TakeSelection = 2,
		// Token: 0x040005A7 RID: 1447
		ExtendSelection = 4,
		// Token: 0x040005A8 RID: 1448
		AddSelection = 8,
		// Token: 0x040005A9 RID: 1449
		RemoveSelection = 16
	}
}
