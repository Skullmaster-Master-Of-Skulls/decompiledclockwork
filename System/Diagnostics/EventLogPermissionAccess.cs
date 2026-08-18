using System;

namespace System.Diagnostics
{
	// Token: 0x02000757 RID: 1879
	[Flags]
	public enum EventLogPermissionAccess
	{
		// Token: 0x040032D1 RID: 13009
		None = 0,
		// Token: 0x040032D2 RID: 13010
		Write = 16,
		// Token: 0x040032D3 RID: 13011
		Administer = 48,
		// Token: 0x040032D4 RID: 13012
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Administer instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Browse = 2,
		// Token: 0x040032D5 RID: 13013
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Write instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Instrument = 6,
		// Token: 0x040032D6 RID: 13014
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Administer instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Audit = 10
	}
}
