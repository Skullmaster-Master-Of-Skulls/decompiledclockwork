using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DAE RID: 3502
	[Guid("3050F670-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementNamespaceTable
	{
		// Token: 0x060174AF RID: 95407
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddNamespace([MarshalAs(UnmanagedType.BStr)] [In] string bstrNamespace, [MarshalAs(UnmanagedType.BStr)] [In] string bstrUrn, [In] int lFlags, [MarshalAs(UnmanagedType.Struct)] [In] ref object pvarFactory);
	}
}
