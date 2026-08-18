using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D4 RID: 1236
	[Guid("3050F835-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLElementCollection3
	{
		// Token: 0x06007AB0 RID: 31408
		[DispId(1506)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object namedItem([MarshalAs(UnmanagedType.BStr)] [In] string name);
	}
}
