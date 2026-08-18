using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007E4 RID: 2020
	[Guid("02fd465d-5c5d-4b7e-95b6-82faa031b74a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IProcessHostFactoryHelper
	{
		// Token: 0x06006081 RID: 24705
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetProcessHost(IProcessHostSupportFunctions functions);
	}
}
