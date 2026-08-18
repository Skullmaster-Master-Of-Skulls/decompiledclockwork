using System;

namespace System.Diagnostics
{
	// Token: 0x02000755 RID: 1877
	public enum EventLogEntryType
	{
		// Token: 0x040032CA RID: 13002
		Error = 1,
		// Token: 0x040032CB RID: 13003
		Warning,
		// Token: 0x040032CC RID: 13004
		Information = 4,
		// Token: 0x040032CD RID: 13005
		SuccessAudit = 8,
		// Token: 0x040032CE RID: 13006
		FailureAudit = 16
	}
}
