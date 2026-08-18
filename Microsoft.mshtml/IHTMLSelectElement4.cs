using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004E1 RID: 1249
	[Guid("3050F838-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLSelectElement4
	{
		// Token: 0x06007F7E RID: 32638
		[DispId(1506)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object namedItem([MarshalAs(UnmanagedType.BStr)] [In] string name);
	}
}
