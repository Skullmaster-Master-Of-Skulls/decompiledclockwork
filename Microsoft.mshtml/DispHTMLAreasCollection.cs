using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008D0 RID: 2256
	[Guid("3050F56A-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[DefaultMember("item")]
	[ComImport]
	public interface DispHTMLAreasCollection
	{
		// Token: 0x17004BC3 RID: 19395
		// (get) Token: 0x0600E616 RID: 58902
		// (set) Token: 0x0600E615 RID: 58901
		[DispId(1500)]
		int length { [DispId(1500)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [DispId(1500)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600E617 RID: 58903
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x0600E618 RID: 58904
		[DispId(0)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object index);

		// Token: 0x0600E619 RID: 58905
		[DispId(1502)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object tags([MarshalAs(UnmanagedType.Struct)] [In] object tagName);

		// Token: 0x0600E61A RID: 58906
		[DispId(1503)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void add([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement element, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object before);

		// Token: 0x0600E61B RID: 58907
		[DispId(1504)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void remove([In] int index = -1);

		// Token: 0x0600E61C RID: 58908
		[DispId(1505)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object urns([MarshalAs(UnmanagedType.Struct)] [In] object urn);

		// Token: 0x0600E61D RID: 58909
		[DispId(1506)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object namedItem([MarshalAs(UnmanagedType.BStr)] [In] string name);
	}
}
