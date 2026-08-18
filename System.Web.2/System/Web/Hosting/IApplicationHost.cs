using System;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	// Token: 0x020007AF RID: 1967
	public interface IApplicationHost
	{
		// Token: 0x06005DB0 RID: 23984
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		string GetVirtualPath();

		// Token: 0x06005DB1 RID: 23985
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		string GetPhysicalPath();

		// Token: 0x06005DB2 RID: 23986
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		IConfigMapPathFactory GetConfigMapPathFactory();

		// Token: 0x06005DB3 RID: 23987
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		IntPtr GetConfigToken();

		// Token: 0x06005DB4 RID: 23988
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		string GetSiteName();

		// Token: 0x06005DB5 RID: 23989
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		string GetSiteID();

		// Token: 0x06005DB6 RID: 23990
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void MessageReceived();
	}
}
