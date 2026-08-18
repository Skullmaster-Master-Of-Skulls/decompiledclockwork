using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200003D RID: 61
	[Guid("2B72133B-3F5B-4602-8952-803546CE3344")]
	[ComImport]
	internal class AppHostWritableAdminManager : IAppHostWritableAdminManager, IAppHostAdminManager
	{
		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern IAppHostElement GetAdminSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionPath);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		public virtual extern object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001FA RID: 506
		[DispId(1610678275)]
		public virtual extern IAppHostConfigManager ConfigManager { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void CommitChanges();

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001FC RID: 508
		// (set) Token: 0x060001FD RID: 509
		[DispId(1610743809)]
		public virtual extern string CommitPath { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x060001FE RID: 510
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AppHostWritableAdminManager();
	}
}
