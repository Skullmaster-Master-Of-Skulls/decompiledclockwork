using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200003E RID: 62
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[Guid("228FB8F7-FB53-4FD5-8C7B-FF59DE606C5B")]
	[ComImport]
	internal class AppHostAdminManager : IAppHostAdminManager
	{
		// Token: 0x060001FF RID: 511
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern IAppHostElement GetAdminSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionPath);

		// Token: 0x06000200 RID: 512
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		public virtual extern object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x06000201 RID: 513
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000202 RID: 514
		[DispId(1610678275)]
		public virtual extern IAppHostConfigManager ConfigManager { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000203 RID: 515
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AppHostAdminManager();
	}
}
