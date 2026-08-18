using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000DC RID: 220
	[Editor("Microsoft.VSDesigner.Data.Design.DataViewRowStateEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Flags]
	public enum DataViewRowState
	{
		// Token: 0x04000445 RID: 1093
		None = 0,
		// Token: 0x04000446 RID: 1094
		Unchanged = 2,
		// Token: 0x04000447 RID: 1095
		Added = 4,
		// Token: 0x04000448 RID: 1096
		Deleted = 8,
		// Token: 0x04000449 RID: 1097
		ModifiedCurrent = 16,
		// Token: 0x0400044A RID: 1098
		ModifiedOriginal = 32,
		// Token: 0x0400044B RID: 1099
		OriginalRows = 42,
		// Token: 0x0400044C RID: 1100
		CurrentRows = 22
	}
}
