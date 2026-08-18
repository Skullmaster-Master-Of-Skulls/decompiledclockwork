using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B6 RID: 694
	[Guid("5BC9C234-6CD7-49bf-A07A-6FDB7F22DFFF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IAppDomainInfo
	{
		// Token: 0x06002401 RID: 9217
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetId();

		// Token: 0x06002402 RID: 9218
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetVirtualPath();

		// Token: 0x06002403 RID: 9219
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetPhysicalPath();

		// Token: 0x06002404 RID: 9220
		[return: MarshalAs(UnmanagedType.I4)]
		int GetSiteId();

		// Token: 0x06002405 RID: 9221
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();
	}
}
