using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200004E RID: 78
	[Guid("EF13D885-642C-4709-99EC-B89561C6BC69")]
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostElementSchema
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600024C RID: 588
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600024D RID: 589
		bool DoesAllowUnschematizedProperties { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600024E RID: 590
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600024F RID: 591
		[DispId(1610678275)]
		IAppHostCollectionSchema CollectionSchema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000250 RID: 592
		[DispId(1610678276)]
		IAppHostElementSchemaCollection ChildElementSchemas { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000251 RID: 593
		[DispId(1610678277)]
		IAppHostPropertySchemaCollection PropertySchemas { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000252 RID: 594
		bool IsCollectionDefault { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
