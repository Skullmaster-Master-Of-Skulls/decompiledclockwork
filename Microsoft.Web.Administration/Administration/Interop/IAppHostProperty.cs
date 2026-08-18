using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000052 RID: 82
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("ED35F7A1-5024-4E7B-A44D-07DDAF4B524D")]
	[ComImport]
	internal interface IAppHostProperty
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000258 RID: 600
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000259 RID: 601
		// (set) Token: 0x0600025A RID: 602
		object Value { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x0600025B RID: 603
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clear();

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600025C RID: 604
		string StringValue { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600025D RID: 605
		IAppHostPropertyException Exception { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600025E RID: 606
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x0600025F RID: 607
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000260 RID: 608
		IAppHostPropertySchema Schema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
