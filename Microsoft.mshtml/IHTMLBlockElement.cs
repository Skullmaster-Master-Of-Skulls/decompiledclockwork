using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004B2 RID: 1202
	[Guid("3050F208-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLBlockElement
	{
		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06005891 RID: 22673
		// (set) Token: 0x06005890 RID: 22672
		[DispId(-2147413096)]
		string clear { [TypeLibFunc(20)] [DispId(-2147413096)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147413096)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
