using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D5 RID: 1237
	[Guid("3050F56B-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[DefaultMember("item")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLElementCollection
	{
		// Token: 0x06007AB1 RID: 31409
		[DispId(1501)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string toString();

		// Token: 0x170029AC RID: 10668
		// (get) Token: 0x06007AB3 RID: 31411
		// (set) Token: 0x06007AB2 RID: 31410
		[DispId(1500)]
		int length { [DispId(1500)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [DispId(1500)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x06007AB4 RID: 31412
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06007AB5 RID: 31413
		[DispId(0)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object index);

		// Token: 0x06007AB6 RID: 31414
		[DispId(1502)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object tags([MarshalAs(UnmanagedType.Struct)] [In] object tagName);

		// Token: 0x06007AB7 RID: 31415
		[DispId(1505)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object urns([MarshalAs(UnmanagedType.Struct)] [In] object urn);

		// Token: 0x06007AB8 RID: 31416
		[DispId(1506)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object namedItem([MarshalAs(UnmanagedType.BStr)] [In] string name);
	}
}
