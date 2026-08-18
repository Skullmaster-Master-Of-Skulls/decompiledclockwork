using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006C RID: 108
	[Guid("3050F4B1-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDOMTextNode
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06000B1A RID: 2842
		// (set) Token: 0x06000B19 RID: 2841
		[DispId(1000)]
		string data { [DispId(1000)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1000)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x06000B1B RID: 2843
		[DispId(1001)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string toString();

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06000B1C RID: 2844
		[DispId(1002)]
		int length { [DispId(1002)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000B1D RID: 2845
		[DispId(1003)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode splitText([In] int offset);
	}
}
