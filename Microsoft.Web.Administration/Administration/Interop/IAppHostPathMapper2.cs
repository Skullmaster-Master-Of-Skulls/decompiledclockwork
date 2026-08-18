using System;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000028 RID: 40
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0f80e901-8f4c-449a-bf90-13d5d082f187")]
	[ComImport]
	internal interface IAppHostPathMapper2
	{
		// Token: 0x060001BC RID: 444
		IntPtr MapPath([MarshalAs(UnmanagedType.BStr)] string bstrVirtualPath, [MarshalAs(UnmanagedType.BStr)] string bstrMappedPhysicalPath, [MarshalAs(UnmanagedType.BStr)] out string bstrNewPhysicalPath);
	}
}
