using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000079 RID: 121
	[TypeLibType(4160)]
	[Guid("3050F80A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLAttributeCollection2
	{
		// Token: 0x06000BCF RID: 3023
		[DispId(1501)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute getNamedItem([MarshalAs(UnmanagedType.BStr)] [In] string bstrName);

		// Token: 0x06000BD0 RID: 3024
		[DispId(1502)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute setNamedItem([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMAttribute ppNode);

		// Token: 0x06000BD1 RID: 3025
		[DispId(1503)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute removeNamedItem([MarshalAs(UnmanagedType.BStr)] [In] string bstrName);
	}
}
