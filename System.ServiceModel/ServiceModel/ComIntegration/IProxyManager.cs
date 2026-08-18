using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200022A RID: 554
	[SuppressUnmanagedCodeSecurity]
	[Guid("C05307A7-70CE-4670-92C9-52A757744A02")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IProxyManager
	{
		// Token: 0x060010B2 RID: 4274
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr dispid);

		// Token: 0x060010B3 RID: 4275
		[PreserveSig]
		int Invoke(uint dispIdMember, IntPtr outerProxy, IntPtr pVarResult, IntPtr pExcepInfo);

		// Token: 0x060010B4 RID: 4276
		[PreserveSig]
		int FindOrCreateProxy(IntPtr outerProxy, ref Guid riid, out IntPtr tearOff);

		// Token: 0x060010B5 RID: 4277
		void TearDownChannels();

		// Token: 0x060010B6 RID: 4278
		[PreserveSig]
		int InterfaceSupportsErrorInfo(ref Guid riid);

		// Token: 0x060010B7 RID: 4279
		[PreserveSig]
		int SupportsDispatch();
	}
}
