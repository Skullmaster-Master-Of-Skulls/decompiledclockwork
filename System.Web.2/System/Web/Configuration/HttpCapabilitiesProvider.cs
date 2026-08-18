using System;

namespace System.Web.Configuration
{
	// Token: 0x020006F3 RID: 1779
	public abstract class HttpCapabilitiesProvider
	{
		// Token: 0x060055E1 RID: 21985
		public abstract HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request);
	}
}
