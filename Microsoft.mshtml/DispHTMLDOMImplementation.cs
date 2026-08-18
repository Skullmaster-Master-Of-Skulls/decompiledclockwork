using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000075 RID: 117
	[InterfaceType(2)]
	[Guid("3050F58F-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLDOMImplementation
	{
		// Token: 0x06000BC8 RID: 3016
		[DispId(1000)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool hasFeature([MarshalAs(UnmanagedType.BStr)] [In] string bstrfeature, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object version);
	}
}
