using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D3 RID: 1235
	[Guid("3050F5EE-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLElementCollection2
	{
		// Token: 0x06007AAF RID: 31407
		[DispId(1505)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object urns([MarshalAs(UnmanagedType.Struct)] [In] object urn);
	}
}
