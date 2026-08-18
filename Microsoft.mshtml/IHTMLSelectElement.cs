using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004DF RID: 1247
	[Guid("3050F244-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[ComImport]
	public interface IHTMLSelectElement : IEnumerable
	{
		// Token: 0x17002B39 RID: 11065
		// (get) Token: 0x06007F66 RID: 32614
		// (set) Token: 0x06007F65 RID: 32613
		[DispId(1002)]
		int size { [TypeLibFunc(20)] [DispId(1002)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1002)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17002B3A RID: 11066
		// (get) Token: 0x06007F68 RID: 32616
		// (set) Token: 0x06007F67 RID: 32615
		[DispId(1003)]
		bool multiple { [DispId(1003)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1003)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17002B3B RID: 11067
		// (get) Token: 0x06007F6A RID: 32618
		// (set) Token: 0x06007F69 RID: 32617
		[DispId(-2147418112)]
		string name { [TypeLibFunc(20)] [DispId(-2147418112)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(-2147418112)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x17002B3C RID: 11068
		// (get) Token: 0x06007F6B RID: 32619
		[DispId(1005)]
		object options { [DispId(1005)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17002B3D RID: 11069
		// (get) Token: 0x06007F6D RID: 32621
		// (set) Token: 0x06007F6C RID: 32620
		[DispId(-2147412082)]
		object onchange { [DispId(-2147412082)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412082)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x17002B3E RID: 11070
		// (get) Token: 0x06007F6F RID: 32623
		// (set) Token: 0x06007F6E RID: 32622
		[DispId(1010)]
		int selectedIndex { [DispId(1010)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1010)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17002B3F RID: 11071
		// (get) Token: 0x06007F70 RID: 32624
		[DispId(1012)]
		string type { [DispId(1012)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17002B40 RID: 11072
		// (get) Token: 0x06007F72 RID: 32626
		// (set) Token: 0x06007F71 RID: 32625
		[DispId(1011)]
		string value { [TypeLibFunc(20)] [DispId(1011)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1011)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x17002B41 RID: 11073
		// (get) Token: 0x06007F74 RID: 32628
		// (set) Token: 0x06007F73 RID: 32627
		[DispId(-2147418036)]
		bool disabled { [DispId(-2147418036)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] get; [TypeLibFunc(20)] [DispId(-2147418036)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17002B42 RID: 11074
		// (get) Token: 0x06007F75 RID: 32629
		[DispId(-2147416108)]
		IHTMLFormElement form { [DispId(-2147416108)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06007F76 RID: 32630
		[DispId(1503)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void add([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement element, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object before);

		// Token: 0x06007F77 RID: 32631
		[DispId(1504)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void remove([In] int index = -1);

		// Token: 0x17002B43 RID: 11075
		// (get) Token: 0x06007F79 RID: 32633
		// (set) Token: 0x06007F78 RID: 32632
		[DispId(1500)]
		int length { [DispId(1500)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1500)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x06007F7A RID: 32634
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06007F7B RID: 32635
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object index);

		// Token: 0x06007F7C RID: 32636
		[DispId(1502)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object tags([MarshalAs(UnmanagedType.Struct)] [In] object tagName);
	}
}
