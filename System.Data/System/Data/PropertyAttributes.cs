using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200025A RID: 602
	[Flags]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("PropertyAttributes has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
	public enum PropertyAttributes
	{
		// Token: 0x04001534 RID: 5428
		NotSupported = 0,
		// Token: 0x04001535 RID: 5429
		Required = 1,
		// Token: 0x04001536 RID: 5430
		Optional = 2,
		// Token: 0x04001537 RID: 5431
		Read = 512,
		// Token: 0x04001538 RID: 5432
		Write = 1024
	}
}
