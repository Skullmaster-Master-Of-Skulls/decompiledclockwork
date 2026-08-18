using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200004C RID: 76
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("64FF8CCC-B287-4DAE-B08A-A72CBF45F453")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IAppHostElement
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600023A RID: 570
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600023B RID: 571
		IAppHostElementCollection Collection { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600023C RID: 572
		IAppHostPropertyCollection Properties { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600023D RID: 573
		IAppHostChildElementCollection ChildElements { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600023E RID: 574
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x0600023F RID: 575
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000240 RID: 576
		IAppHostElementSchema Schema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000241 RID: 577
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElement GetElementByName([MarshalAs(UnmanagedType.BStr)] [In] string bstrSubName);

		// Token: 0x06000242 RID: 578
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostProperty GetPropertyByName([MarshalAs(UnmanagedType.BStr)] [In] string bstrSubName);

		// Token: 0x06000243 RID: 579
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clear();

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000244 RID: 580
		IAppHostMethodCollection Methods { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
