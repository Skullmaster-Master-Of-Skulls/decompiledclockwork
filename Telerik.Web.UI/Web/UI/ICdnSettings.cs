using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001818 RID: 6168
	internal interface ICdnSettings
	{
		// Token: 0x170048A9 RID: 18601
		// (get) Token: 0x0600F029 RID: 61481
		string BaseUrl { get; }

		// Token: 0x170048AA RID: 18602
		// (get) Token: 0x0600F02A RID: 61482
		string BaseSecureUrl { get; }

		// Token: 0x170048AB RID: 18603
		// (get) Token: 0x0600F02B RID: 61483
		string BasePath { get; }

		// Token: 0x170048AC RID: 18604
		// (get) Token: 0x0600F02C RID: 61484
		string BaseCompressedPath { get; }
	}
}
