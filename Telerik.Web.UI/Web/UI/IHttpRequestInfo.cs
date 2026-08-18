using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001819 RID: 6169
	public interface IHttpRequestInfo
	{
		// Token: 0x170048AD RID: 18605
		// (get) Token: 0x0600F02D RID: 61485
		bool IsSecure { get; }

		// Token: 0x170048AE RID: 18606
		// (get) Token: 0x0600F02E RID: 61486
		bool SupportsGzip { get; }
	}
}
