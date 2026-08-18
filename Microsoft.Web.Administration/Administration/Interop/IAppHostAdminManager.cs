using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200003B RID: 59
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9BE77978-73ED-4A9A-87FD-13F09FEC1B13")]
	[ComImport]
	internal interface IAppHostAdminManager
	{
		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElement GetAdminSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPath);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001EF RID: 495
		[DispId(1610678275)]
		IAppHostConfigManager ConfigManager { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
