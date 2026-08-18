using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006E RID: 110
	[TypeLibType(4160)]
	[Guid("3050F80D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDOMImplementation
	{
		// Token: 0x06000B23 RID: 2851
		[DispId(1000)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool hasFeature([MarshalAs(UnmanagedType.BStr)] [In] string bstrfeature, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object version);
	}
}
