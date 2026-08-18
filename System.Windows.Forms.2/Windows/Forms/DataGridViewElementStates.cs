using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020001D6 RID: 470
	[Flags]
	[ComVisible(true)]
	public enum DataGridViewElementStates
	{
		// Token: 0x04000DC1 RID: 3521
		None = 0,
		// Token: 0x04000DC2 RID: 3522
		Displayed = 1,
		// Token: 0x04000DC3 RID: 3523
		Frozen = 2,
		// Token: 0x04000DC4 RID: 3524
		ReadOnly = 4,
		// Token: 0x04000DC5 RID: 3525
		Resizable = 8,
		// Token: 0x04000DC6 RID: 3526
		ResizableSet = 16,
		// Token: 0x04000DC7 RID: 3527
		Selected = 32,
		// Token: 0x04000DC8 RID: 3528
		Visible = 64
	}
}
