using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007D7 RID: 2007
	[Guid("0ccd465e-3114-4ca3-ad50-cea561307e93")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IProcessHost
	{
		// Token: 0x06006032 RID: 24626
		void StartApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string appPath, [MarshalAs(UnmanagedType.Interface)] out object runtimeInterface);

		// Token: 0x06006033 RID: 24627
		void ShutdownApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId);

		// Token: 0x06006034 RID: 24628
		void Shutdown();

		// Token: 0x06006035 RID: 24629
		void EnumerateAppDomains([MarshalAs(UnmanagedType.Interface)] out IAppDomainInfoEnum appDomainInfoEnum);
	}
}
