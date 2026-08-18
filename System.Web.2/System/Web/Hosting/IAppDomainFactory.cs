using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x0200079D RID: 1949
	[Guid("e6e21054-a7dc-4378-877d-b7f4a2d7e8ba")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAppDomainFactory
	{
		// Token: 0x06005CB0 RID: 23728
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object Create([MarshalAs(UnmanagedType.BStr)] [In] string module, [MarshalAs(UnmanagedType.BStr)] [In] string typeName, [MarshalAs(UnmanagedType.BStr)] [In] string appId, [MarshalAs(UnmanagedType.BStr)] [In] string appPath, [MarshalAs(UnmanagedType.BStr)] [In] string strUrlOfAppOrigin, [MarshalAs(UnmanagedType.I4)] [In] int iZone);
	}
}
