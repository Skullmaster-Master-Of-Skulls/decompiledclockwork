using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007DC RID: 2012
	[Guid("5BC9C234-6CD7-49bf-A07A-6FDB7F22DFFF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAppDomainInfo
	{
		// Token: 0x06006041 RID: 24641
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetId();

		// Token: 0x06006042 RID: 24642
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetVirtualPath();

		// Token: 0x06006043 RID: 24643
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetPhysicalPath();

		// Token: 0x06006044 RID: 24644
		[return: MarshalAs(UnmanagedType.I4)]
		int GetSiteId();

		// Token: 0x06006045 RID: 24645
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();
	}
}
