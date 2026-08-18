using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000391 RID: 913
	[TypeLibType(4160)]
	[Guid("3050F1D9-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFontElement
	{
		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003605 RID: 13829
		// (set) Token: 0x06003604 RID: 13828
		[DispId(-2147413110)]
		object color { [TypeLibFunc(20)] [DispId(-2147413110)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147413110)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003607 RID: 13831
		// (set) Token: 0x06003606 RID: 13830
		[DispId(-2147413094)]
		string face { [TypeLibFunc(20)] [DispId(-2147413094)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147413094)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003609 RID: 13833
		// (set) Token: 0x06003608 RID: 13832
		[DispId(-2147413093)]
		object size { [DispId(-2147413093)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147413093)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] [param: In] set; }
	}
}
