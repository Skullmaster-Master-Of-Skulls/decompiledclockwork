using System;

namespace System.Web.Util
{
	// Token: 0x020001E0 RID: 480
	internal interface ISyncContext
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060017AE RID: 6062
		HttpContext HttpContext { get; }

		// Token: 0x060017AF RID: 6063
		ISyncContextLock Enter();
	}
}
