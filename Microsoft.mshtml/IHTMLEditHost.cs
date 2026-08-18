using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC5 RID: 3269
	[Guid("3050F6A0-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLEditHost
	{
		// Token: 0x06016303 RID: 90883
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SnapRect([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement, [In] [Out] ref tagRECT prcNew, [In] _ELEMENT_CORNER eHandle);
	}
}
