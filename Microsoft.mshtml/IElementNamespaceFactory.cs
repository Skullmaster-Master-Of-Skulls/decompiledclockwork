using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DAF RID: 3503
	[InterfaceType(1)]
	[Guid("3050F672-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementNamespaceFactory
	{
		// Token: 0x060174B0 RID: 95408
		[MethodImpl(MethodImplOptions.InternalCall)]
		void create([MarshalAs(UnmanagedType.Interface)] [In] IElementNamespace pNamespace);
	}
}
