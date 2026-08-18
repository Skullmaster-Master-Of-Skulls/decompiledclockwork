using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007E0 RID: 2016
	[Guid("AE54F424-71BC-4da5-AA2F-8C0CD53496FC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IApplicationPreloadManager
	{
		// Token: 0x06006055 RID: 24661
		void SetApplicationPreloadUtil([MarshalAs(UnmanagedType.Interface)] [In] IApplicationPreloadUtil preloadUtil);

		// Token: 0x06006056 RID: 24662
		void SetApplicationPreloadState([MarshalAs(UnmanagedType.LPWStr)] [In] string context, [MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.Bool)] [In] bool enabled);
	}
}
