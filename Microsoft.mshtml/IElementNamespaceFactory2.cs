using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB0 RID: 3504
	[Guid("3050F805-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementNamespaceFactory2 : IElementNamespaceFactory
	{
		// Token: 0x060174B1 RID: 95409
		[MethodImpl(MethodImplOptions.InternalCall)]
		void create([MarshalAs(UnmanagedType.Interface)] [In] IElementNamespace pNamespace);

		// Token: 0x060174B2 RID: 95410
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateWithImplementation([MarshalAs(UnmanagedType.Interface)] [In] IElementNamespace pNamespace, [MarshalAs(UnmanagedType.BStr)] [In] string bstrImplementation);
	}
}
