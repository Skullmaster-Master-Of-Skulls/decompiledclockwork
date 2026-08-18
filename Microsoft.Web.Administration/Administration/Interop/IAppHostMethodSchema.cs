using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000040 RID: 64
	[Guid("2D9915FB-9D42-4328-B782-1B46819FAB9E")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IAppHostMethodSchema
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000207 RID: 519
		[DispId(1610678272)]
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000208 RID: 520
		[DispId(1610678273)]
		IAppHostElementSchema InputSchema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000209 RID: 521
		[DispId(1610678274)]
		IAppHostElementSchema OutputSchema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600020A RID: 522
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);
	}
}
