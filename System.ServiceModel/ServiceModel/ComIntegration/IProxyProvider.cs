using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200022B RID: 555
	[SuppressUnmanagedCodeSecurity]
	[Guid("11281BB7-1253-45ef-B98F-D551F79499FD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IProxyProvider
	{
		// Token: 0x060010B8 RID: 4280
		[PreserveSig]
		int CreateOuterProxyInstance(IProxyManager proxyManager, [In] ref Guid riid, out IntPtr ppv);

		// Token: 0x060010B9 RID: 4281
		[PreserveSig]
		int CreateDispatchProxyInstance(IntPtr outer, IPseudoDispatch proxy, out IntPtr ppvInner);
	}
}
