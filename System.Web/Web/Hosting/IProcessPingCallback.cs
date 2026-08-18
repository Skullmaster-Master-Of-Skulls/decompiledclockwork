using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000299 RID: 665
	[Guid("f11dc4c9-ddd1-4566-ad53-cf6f3a28fefe")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IProcessPingCallback
	{
		// Token: 0x060022D6 RID: 8918
		void Respond();
	}
}
