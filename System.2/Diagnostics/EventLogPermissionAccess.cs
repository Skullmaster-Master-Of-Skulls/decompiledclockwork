using System;

namespace System.Diagnostics
{
	// Token: 0x020004D1 RID: 1233
	[Flags]
	public enum EventLogPermissionAccess
	{
		// Token: 0x04002779 RID: 10105
		None = 0,
		// Token: 0x0400277A RID: 10106
		Write = 16,
		// Token: 0x0400277B RID: 10107
		Administer = 48,
		// Token: 0x0400277C RID: 10108
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Administer instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Browse = 2,
		// Token: 0x0400277D RID: 10109
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Write instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Instrument = 6,
		// Token: 0x0400277E RID: 10110
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Administer instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Audit = 10
	}
}
