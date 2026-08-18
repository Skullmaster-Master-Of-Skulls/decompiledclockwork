using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD5 RID: 3285
	[TypeLibType(4160)]
	[Guid("3050F6BB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLNamespace
	{
		// Token: 0x170075B9 RID: 30137
		// (get) Token: 0x06016335 RID: 90933
		[DispId(1000)]
		string name { [DispId(1000)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170075BA RID: 30138
		// (get) Token: 0x06016336 RID: 90934
		[DispId(1001)]
		string urn { [TypeLibFunc(4)] [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170075BB RID: 30139
		// (get) Token: 0x06016337 RID: 90935
		[DispId(1002)]
		object tagNames { [DispId(1002)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x170075BC RID: 30140
		// (get) Token: 0x06016338 RID: 90936
		[DispId(-2147412996)]
		object readyState { [DispId(-2147412996)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; }

		// Token: 0x170075BD RID: 30141
		// (get) Token: 0x0601633A RID: 90938
		// (set) Token: 0x06016339 RID: 90937
		[DispId(-2147412087)]
		object onreadystatechange { [TypeLibFunc(20)] [DispId(-2147412087)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412087)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x0601633B RID: 90939
		[DispId(1003)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void doImport([MarshalAs(UnmanagedType.BStr)] [In] string bstrImplementationUrl);

		// Token: 0x0601633C RID: 90940
		[DispId(-2147417605)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool attachEvent([MarshalAs(UnmanagedType.BStr)] [In] string @event, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

		// Token: 0x0601633D RID: 90941
		[DispId(-2147417604)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void detachEvent([MarshalAs(UnmanagedType.BStr)] [In] string @event, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);
	}
}
