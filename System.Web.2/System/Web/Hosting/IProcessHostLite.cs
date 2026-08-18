using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;

namespace System.Web.Hosting
{
	// Token: 0x020007D8 RID: 2008
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E2A1F244-70EB-483A-ACC8-DE6ACE5BF8B1")]
	[ComImport]
	internal interface IProcessHostLite
	{
		// Token: 0x06006036 RID: 24630
		[return: MarshalAs(UnmanagedType.Interface)]
		IObjectHandle GetCustomLoader([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string appConfigPath, [MarshalAs(UnmanagedType.Interface)] out IProcessHostSupportFunctions supportFunctions, [MarshalAs(UnmanagedType.Interface)] out AppDomain newlyCreatedAppDomain);

		// Token: 0x06006037 RID: 24631
		void ReportCustomLoaderError([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [In] int hr, [MarshalAs(UnmanagedType.Interface)] [In] AppDomain newlyCreatedAppDomain);

		// Token: 0x06006038 RID: 24632
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetFullExceptionMessage([In] int hr, [In] IntPtr pErrorInfo);
	}
}
