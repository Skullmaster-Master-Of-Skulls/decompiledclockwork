using System;

namespace System.Web.WebPages
{
	// Token: 0x02000035 RID: 53
	public interface IDisplayMode
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000173 RID: 371
		string DisplayModeId { get; }

		// Token: 0x06000174 RID: 372
		bool CanHandleContext(HttpContextBase httpContext);

		// Token: 0x06000175 RID: 373
		DisplayInfo GetDisplayInfo(HttpContextBase httpContext, string virtualPath, Func<string, bool> virtualPathExists);
	}
}
