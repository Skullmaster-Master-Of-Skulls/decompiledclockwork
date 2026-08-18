using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB1 RID: 3505
	[InterfaceType(1)]
	[Guid("3050F7FD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementNamespaceFactoryCallback
	{
		// Token: 0x060174B3 RID: 95411
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Resolve([MarshalAs(UnmanagedType.BStr)] [In] string bstrNamespace, [MarshalAs(UnmanagedType.BStr)] [In] string bstrTagName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrAttrs, [MarshalAs(UnmanagedType.Interface)] [In] IElementNamespace pNamespace);
	}
}
