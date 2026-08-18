using System;

namespace System.Diagnostics
{
	// Token: 0x020004EA RID: 1258
	[Flags]
	public enum PerformanceCounterPermissionAccess
	{
		// Token: 0x04002809 RID: 10249
		[Obsolete("This member has been deprecated.  Use System.Diagnostics.PerformanceCounter.PerformanceCounterPermissionAccess.Read instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Browse = 1,
		// Token: 0x0400280A RID: 10250
		[Obsolete("This member has been deprecated.  Use System.Diagnostics.PerformanceCounter.PerformanceCounterPermissionAccess.Write instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Instrument = 3,
		// Token: 0x0400280B RID: 10251
		None = 0,
		// Token: 0x0400280C RID: 10252
		Read = 1,
		// Token: 0x0400280D RID: 10253
		Write = 2,
		// Token: 0x0400280E RID: 10254
		Administer = 7
	}
}
