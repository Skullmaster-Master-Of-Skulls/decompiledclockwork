using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005BB RID: 1467
	[Flags]
	public enum SaveOptions
	{
		// Token: 0x0400163F RID: 5695
		None = 0,
		// Token: 0x04001640 RID: 5696
		AcceptAllChangesAfterSave = 1,
		// Token: 0x04001641 RID: 5697
		DetectChangesBeforeSave = 2
	}
}
