using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B48 RID: 2888
	[Guid("3050F82E-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLFrameBase3
	{
		// Token: 0x170064D4 RID: 25812
		// (get) Token: 0x06013081 RID: 77953
		// (set) Token: 0x06013080 RID: 77952
		[DispId(-2147415102)]
		string longDesc { [TypeLibFunc(20)] [DispId(-2147415102)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147415102)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
