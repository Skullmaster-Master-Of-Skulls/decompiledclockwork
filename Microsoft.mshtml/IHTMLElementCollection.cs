using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200008F RID: 143
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLElementCollection : IEnumerable
	{
		// Token: 0x06000C6F RID: 3183
		[DispId(1501)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string toString();

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06000C71 RID: 3185
		// (set) Token: 0x06000C70 RID: 3184
		[DispId(1500)]
		int length { [DispId(1500)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1500)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x06000C72 RID: 3186
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06000C73 RID: 3187
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object index);

		// Token: 0x06000C74 RID: 3188
		[DispId(1502)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object tags([MarshalAs(UnmanagedType.Struct)] [In] object tagName);
	}
}
