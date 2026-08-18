using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	// Token: 0x02000232 RID: 562
	[Editor("System.Windows.Forms.Design.DockEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum DockStyle
	{
		// Token: 0x04000F06 RID: 3846
		None,
		// Token: 0x04000F07 RID: 3847
		Top,
		// Token: 0x04000F08 RID: 3848
		Bottom,
		// Token: 0x04000F09 RID: 3849
		Left,
		// Token: 0x04000F0A RID: 3850
		Right,
		// Token: 0x04000F0B RID: 3851
		Fill
	}
}
