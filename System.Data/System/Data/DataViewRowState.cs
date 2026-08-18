using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000AD RID: 173
	[Flags]
	[Editor("Microsoft.VSDesigner.Data.Design.DataViewRowStateEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public enum DataViewRowState
	{
		// Token: 0x04000869 RID: 2153
		None = 0,
		// Token: 0x0400086A RID: 2154
		Unchanged = 2,
		// Token: 0x0400086B RID: 2155
		Added = 4,
		// Token: 0x0400086C RID: 2156
		Deleted = 8,
		// Token: 0x0400086D RID: 2157
		ModifiedCurrent = 16,
		// Token: 0x0400086E RID: 2158
		ModifiedOriginal = 32,
		// Token: 0x0400086F RID: 2159
		OriginalRows = 42,
		// Token: 0x04000870 RID: 2160
		CurrentRows = 22
	}
}
