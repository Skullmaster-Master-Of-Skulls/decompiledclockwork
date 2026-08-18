using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.SessionState
{
	// Token: 0x02000137 RID: 311
	[Guid("7297744b-e188-40bf-b7e9-56698d25cf44")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IStateRuntime
	{
		// Token: 0x060012B3 RID: 4787
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void StopProcessing();

		// Token: 0x060012B4 RID: 4788
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void ProcessRequest([MarshalAs(UnmanagedType.SysInt)] [In] IntPtr tracker, [MarshalAs(UnmanagedType.I4)] [In] int verb, [MarshalAs(UnmanagedType.LPWStr)] [In] string uri, [MarshalAs(UnmanagedType.I4)] [In] int exclusive, [MarshalAs(UnmanagedType.I4)] [In] int timeout, [MarshalAs(UnmanagedType.I4)] [In] int lockCookieExists, [MarshalAs(UnmanagedType.I4)] [In] int lockCookie, [MarshalAs(UnmanagedType.I4)] [In] int contentLength, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr content);

		// Token: 0x060012B5 RID: 4789
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void ProcessRequest([MarshalAs(UnmanagedType.SysInt)] [In] IntPtr tracker, [MarshalAs(UnmanagedType.I4)] [In] int verb, [MarshalAs(UnmanagedType.LPWStr)] [In] string uri, [MarshalAs(UnmanagedType.I4)] [In] int exclusive, [MarshalAs(UnmanagedType.I4)] [In] int extraFlags, [MarshalAs(UnmanagedType.I4)] [In] int timeout, [MarshalAs(UnmanagedType.I4)] [In] int lockCookieExists, [MarshalAs(UnmanagedType.I4)] [In] int lockCookie, [MarshalAs(UnmanagedType.I4)] [In] int contentLength, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr content);
	}
}
