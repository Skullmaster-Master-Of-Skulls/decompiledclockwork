using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001AC RID: 428
	[Flags]
	public enum SqlBulkCopyOptions
	{
		// Token: 0x04000EF1 RID: 3825
		Default = 0,
		// Token: 0x04000EF2 RID: 3826
		KeepIdentity = 1,
		// Token: 0x04000EF3 RID: 3827
		CheckConstraints = 2,
		// Token: 0x04000EF4 RID: 3828
		TableLock = 4,
		// Token: 0x04000EF5 RID: 3829
		KeepNulls = 8,
		// Token: 0x04000EF6 RID: 3830
		FireTriggers = 16,
		// Token: 0x04000EF7 RID: 3831
		UseInternalTransaction = 32,
		// Token: 0x04000EF8 RID: 3832
		AllowEncryptedValueModifications = 64
	}
}
