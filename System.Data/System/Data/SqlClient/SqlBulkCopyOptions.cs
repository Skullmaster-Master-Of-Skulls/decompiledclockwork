using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002B9 RID: 697
	[Flags]
	public enum SqlBulkCopyOptions
	{
		// Token: 0x04001703 RID: 5891
		Default = 0,
		// Token: 0x04001704 RID: 5892
		KeepIdentity = 1,
		// Token: 0x04001705 RID: 5893
		CheckConstraints = 2,
		// Token: 0x04001706 RID: 5894
		TableLock = 4,
		// Token: 0x04001707 RID: 5895
		KeepNulls = 8,
		// Token: 0x04001708 RID: 5896
		FireTriggers = 16,
		// Token: 0x04001709 RID: 5897
		UseInternalTransaction = 32
	}
}
