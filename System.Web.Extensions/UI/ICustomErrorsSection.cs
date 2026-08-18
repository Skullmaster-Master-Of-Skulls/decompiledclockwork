using System;
using System.Web.Configuration;

namespace System.Web.UI
{
	// Token: 0x02000056 RID: 86
	internal interface ICustomErrorsSection
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600030D RID: 781
		string DefaultRedirect { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600030E RID: 782
		CustomErrorCollection Errors { get; }
	}
}
