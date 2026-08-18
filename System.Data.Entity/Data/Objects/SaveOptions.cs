using System;

namespace System.Data.Objects
{
	// Token: 0x02000154 RID: 340
	[Flags]
	public enum SaveOptions
	{
		// Token: 0x04000AE3 RID: 2787
		None = 0,
		// Token: 0x04000AE4 RID: 2788
		AcceptAllChangesAfterSave = 1,
		// Token: 0x04000AE5 RID: 2789
		DetectChangesBeforeSave = 2
	}
}
