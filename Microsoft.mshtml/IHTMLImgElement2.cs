using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200027C RID: 636
	[Guid("3050F826-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLImgElement2
	{
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002851 RID: 10321
		// (set) Token: 0x06002850 RID: 10320
		[DispId(2019)]
		string longDesc { [TypeLibFunc(20)] [DispId(2019)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(2019)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
