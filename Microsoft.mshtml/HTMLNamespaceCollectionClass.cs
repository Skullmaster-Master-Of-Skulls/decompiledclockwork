using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CDB RID: 3291
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F6B9-98B5-11CF-BB82-00AA00BDCE0B")]
	[DefaultMember("item")]
	[ComImport]
	public class HTMLNamespaceCollectionClass : IHTMLNamespaceCollection, HTMLNamespaceCollection
	{
		// Token: 0x06016351 RID: 90961
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern HTMLNamespaceCollectionClass();

		// Token: 0x170075C4 RID: 30148
		// (get) Token: 0x06016352 RID: 90962
		[DispId(1000)]
		public virtual extern int length { [DispId(1000)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06016353 RID: 90963
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		public virtual extern object item([MarshalAs(UnmanagedType.Struct)] [In] object index);

		// Token: 0x06016354 RID: 90964
		[DispId(1001)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		public virtual extern object add([MarshalAs(UnmanagedType.BStr)] [In] string bstrNamespace, [MarshalAs(UnmanagedType.BStr)] [In] string bstrUrn, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object implementationUrl);
	}
}
