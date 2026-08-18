using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C96 RID: 3222
	[InterfaceType(1)]
	[Guid("3050F5F9-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupContainer
	{
		// Token: 0x06016201 RID: 90625
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OwningDoc([MarshalAs(UnmanagedType.Interface)] out IHTMLDocument2 ppDoc);
	}
}
