using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200004D RID: 77
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C8550BFF-5281-4B1E-AC34-99B6FA38464D")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IAppHostElementCollection
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000245 RID: 581
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700010C RID: 268
		IAppHostElement this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000247 RID: 583
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddElement([MarshalAs(UnmanagedType.Interface)] [In] IAppHostElement pElement, [In] [Optional] int cPosition);

		// Token: 0x06000248 RID: 584
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteElement([MarshalAs(UnmanagedType.Struct)] [In] object cIndex);

		// Token: 0x06000249 RID: 585
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clear();

		// Token: 0x0600024A RID: 586
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElement CreateNewElement([MarshalAs(UnmanagedType.BStr)] [In] [Optional] string bstrElementName);

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600024B RID: 587
		IAppHostCollectionSchema Schema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
