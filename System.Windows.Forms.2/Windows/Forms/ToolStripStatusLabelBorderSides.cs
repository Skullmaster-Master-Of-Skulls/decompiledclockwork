using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000407 RID: 1031
	[ComVisible(true)]
	[Editor("System.Windows.Forms.Design.BorderSidesEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Flags]
	public enum ToolStripStatusLabelBorderSides
	{
		// Token: 0x040026E0 RID: 9952
		All = 15,
		// Token: 0x040026E1 RID: 9953
		Bottom = 8,
		// Token: 0x040026E2 RID: 9954
		Left = 1,
		// Token: 0x040026E3 RID: 9955
		Right = 4,
		// Token: 0x040026E4 RID: 9956
		Top = 2,
		// Token: 0x040026E5 RID: 9957
		None = 0
	}
}
