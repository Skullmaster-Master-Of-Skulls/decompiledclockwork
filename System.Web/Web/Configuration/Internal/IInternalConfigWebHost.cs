using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Configuration.Internal
{
	// Token: 0x02000267 RID: 615
	[ComVisible(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public interface IInternalConfigWebHost
	{
		// Token: 0x0600204D RID: 8269
		void GetSiteIDAndVPathFromConfigPath(string configPath, out string siteID, out string vpath);

		// Token: 0x0600204E RID: 8270
		string GetConfigPathFromSiteIDAndVPath(string siteID, string vpath);
	}
}
