using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x0200079F RID: 1951
	[Guid("02998279-7175-4d59-aa5a-fb8e44d4ca9d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAppManagerAppDomainFactory
	{
		// Token: 0x06005CB3 RID: 23731
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object Create([MarshalAs(UnmanagedType.BStr)] [In] string appId, [MarshalAs(UnmanagedType.BStr)] [In] string appPath);

		// Token: 0x06005CB4 RID: 23732
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		void Stop();
	}
}
