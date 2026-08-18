using System;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000051 RID: 81
	[Guid("e7927575-5cc3-403b-822e-328a6b904bee")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostPathMapper
	{
		// Token: 0x06000257 RID: 599
		[return: MarshalAs(UnmanagedType.BStr)]
		string MapPath([MarshalAs(UnmanagedType.BStr)] string bstrVirtualPath, [MarshalAs(UnmanagedType.BStr)] string bstrMappedPhysicalPath);
	}
}
