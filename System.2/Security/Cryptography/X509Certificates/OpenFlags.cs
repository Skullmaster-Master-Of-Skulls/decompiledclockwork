using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200047F RID: 1151
	[Flags]
	public enum OpenFlags
	{
		// Token: 0x0400264E RID: 9806
		ReadOnly = 0,
		// Token: 0x0400264F RID: 9807
		ReadWrite = 1,
		// Token: 0x04002650 RID: 9808
		MaxAllowed = 2,
		// Token: 0x04002651 RID: 9809
		OpenExistingOnly = 4,
		// Token: 0x04002652 RID: 9810
		IncludeArchived = 8
	}
}
