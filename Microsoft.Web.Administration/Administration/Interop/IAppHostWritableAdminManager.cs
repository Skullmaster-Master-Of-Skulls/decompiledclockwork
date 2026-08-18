using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200003C RID: 60
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FA7660F6-7B3F-4237-A8BF-ED0AD0DCBBD9")]
	[ComImport]
	internal interface IAppHostWritableAdminManager : IAppHostAdminManager
	{
		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElement GetAdminSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrPath);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x060001F2 RID: 498
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001F3 RID: 499
		[DispId(1610678275)]
		IAppHostConfigManager ConfigManager { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x060001F4 RID: 500
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CommitChanges();

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001F5 RID: 501
		// (set) Token: 0x060001F6 RID: 502
		string CommitPath { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
