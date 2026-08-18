using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x02000602 RID: 1538
	[ComVisible(true)]
	public enum ViewTechnology
	{
		// Token: 0x04002B74 RID: 11124
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Passthrough,
		// Token: 0x04002B75 RID: 11125
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		WindowsForms,
		// Token: 0x04002B76 RID: 11126
		Default
	}
}
