using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000777 RID: 1911
	[TypeLibType(4160)]
	[Guid("3050F207-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLNextIdElement
	{
		// Token: 0x1700385C RID: 14428
		// (get) Token: 0x0600AF92 RID: 44946
		// (set) Token: 0x0600AF91 RID: 44945
		[DispId(1012)]
		string n { [TypeLibFunc(20)] [DispId(1012)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1012)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
