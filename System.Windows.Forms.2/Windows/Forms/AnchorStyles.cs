using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	// Token: 0x02000120 RID: 288
	[Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Flags]
	public enum AnchorStyles
	{
		// Token: 0x040005D2 RID: 1490
		Top = 1,
		// Token: 0x040005D3 RID: 1491
		Bottom = 2,
		// Token: 0x040005D4 RID: 1492
		Left = 4,
		// Token: 0x040005D5 RID: 1493
		Right = 8,
		// Token: 0x040005D6 RID: 1494
		None = 0
	}
}
