using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000281 RID: 641
	[Guid("02998279-7175-4d59-aa5a-fb8e44d4ca9d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IAppManagerAppDomainFactory
	{
		// Token: 0x06002111 RID: 8465
		[return: MarshalAs(UnmanagedType.Interface)]
		object Create([MarshalAs(UnmanagedType.BStr)] [In] string appId, [MarshalAs(UnmanagedType.BStr)] [In] string appPath);

		// Token: 0x06002112 RID: 8466
		void Stop();
	}
}
