using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200004B RID: 75
	[Guid("8F6D760F-F0CB-4D69-B5F6-848B33E9BDC6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostConfigManager
	{
		// Token: 0x06000238 RID: 568
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostConfigFile GetConfigFile([MarshalAs(UnmanagedType.BStr)] [In] string bstrConfigPath);

		// Token: 0x06000239 RID: 569
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetUniqueConfigPath([MarshalAs(UnmanagedType.BStr)] [In] string bstrConfigPath);
	}
}
