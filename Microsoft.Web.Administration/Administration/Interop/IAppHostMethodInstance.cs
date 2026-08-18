using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000042 RID: 66
	[Guid("B80F3C42-60E0-4AE0-9007-F52852D3DBED")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IAppHostMethodInstance
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600020D RID: 525
		IAppHostElement Input { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600020E RID: 526
		IAppHostElement Output { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600020F RID: 527
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Execute();

		// Token: 0x06000210 RID: 528
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x06000211 RID: 529
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);
	}
}
