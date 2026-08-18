using System;

namespace System.Diagnostics
{
	// Token: 0x02000770 RID: 1904
	[Flags]
	public enum PerformanceCounterPermissionAccess
	{
		// Token: 0x0400335D RID: 13149
		[Obsolete("This member has been deprecated.  Use System.Diagnostics.PerformanceCounter.PerformanceCounterPermissionAccess.Read instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Browse = 1,
		// Token: 0x0400335E RID: 13150
		[Obsolete("This member has been deprecated.  Use System.Diagnostics.PerformanceCounter.PerformanceCounterPermissionAccess.Write instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Instrument = 3,
		// Token: 0x0400335F RID: 13151
		None = 0,
		// Token: 0x04003360 RID: 13152
		Read = 1,
		// Token: 0x04003361 RID: 13153
		Write = 2,
		// Token: 0x04003362 RID: 13154
		Administer = 7
	}
}
