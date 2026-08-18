using System;

namespace System.Diagnostics
{
	// Token: 0x020004CF RID: 1231
	public enum EventLogEntryType
	{
		// Token: 0x04002772 RID: 10098
		Error = 1,
		// Token: 0x04002773 RID: 10099
		Warning,
		// Token: 0x04002774 RID: 10100
		Information = 4,
		// Token: 0x04002775 RID: 10101
		SuccessAudit = 8,
		// Token: 0x04002776 RID: 10102
		FailureAudit = 16
	}
}
