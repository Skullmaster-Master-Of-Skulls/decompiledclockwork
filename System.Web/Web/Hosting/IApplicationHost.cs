using System;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	// Token: 0x0200028F RID: 655
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public interface IApplicationHost
	{
		// Token: 0x060021BF RID: 8639
		string GetVirtualPath();

		// Token: 0x060021C0 RID: 8640
		string GetPhysicalPath();

		// Token: 0x060021C1 RID: 8641
		IConfigMapPathFactory GetConfigMapPathFactory();

		// Token: 0x060021C2 RID: 8642
		IntPtr GetConfigToken();

		// Token: 0x060021C3 RID: 8643
		string GetSiteName();

		// Token: 0x060021C4 RID: 8644
		string GetSiteID();

		// Token: 0x060021C5 RID: 8645
		void MessageReceived();
	}
}
