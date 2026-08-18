using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007E1 RID: 2017
	[Guid("940D8ADD-9E40-4475-9A67-2CDCDF57995C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IApplicationPreloadUtil
	{
		// Token: 0x06006057 RID: 24663
		void GetApplicationPreloadInfo([MarshalAs(UnmanagedType.LPWStr)] [In] string context, [MarshalAs(UnmanagedType.Bool)] out bool enabled, [MarshalAs(UnmanagedType.BStr)] out string startupObjType, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] parametersForStartupObj);

		// Token: 0x06006058 RID: 24664
		void ReportApplicationPreloadFailure([MarshalAs(UnmanagedType.LPWStr)] [In] string context, [MarshalAs(UnmanagedType.U4)] [In] int errorCode, [MarshalAs(UnmanagedType.LPWStr)] [In] string errorMessage);
	}
}
