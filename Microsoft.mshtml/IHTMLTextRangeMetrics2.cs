using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001DE RID: 478
	[Guid("3050F4A6-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTextRangeMetrics2
	{
		// Token: 0x06001B4E RID: 6990
		[DispId(1041)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLRectCollection getClientRects();

		// Token: 0x06001B4F RID: 6991
		[DispId(1042)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLRect getBoundingClientRect();
	}
}
