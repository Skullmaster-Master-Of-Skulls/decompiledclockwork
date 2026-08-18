using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200022D RID: 557
	[Guid("0000013E-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServerSecurity
	{
		// Token: 0x060010BC RID: 4284
		void QueryBlanket(IntPtr authnSvc, IntPtr authzSvc, IntPtr serverPrincipalName, IntPtr authnLevel, IntPtr impLevel, IntPtr clientPrincipalName, IntPtr Capabilities);

		// Token: 0x060010BD RID: 4285
		[PreserveSig]
		int ImpersonateClient();

		// Token: 0x060010BE RID: 4286
		[PreserveSig]
		int RevertToSelf();

		// Token: 0x060010BF RID: 4287
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsImpersonating();
	}
}
