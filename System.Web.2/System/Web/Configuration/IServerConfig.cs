using System;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x0200070F RID: 1807
	internal interface IServerConfig
	{
		// Token: 0x06005711 RID: 22289
		string MapPath(IApplicationHost appHost, VirtualPath path);

		// Token: 0x06005712 RID: 22290
		string GetSiteNameFromSiteID(string siteID);

		// Token: 0x06005713 RID: 22291
		bool GetUncUser(IApplicationHost appHost, VirtualPath path, out string username, out string password);

		// Token: 0x06005714 RID: 22292
		string[] GetVirtualSubdirs(VirtualPath path, bool inApp);

		// Token: 0x06005715 RID: 22293
		long GetW3WPMemoryLimitInKB();
	}
}
