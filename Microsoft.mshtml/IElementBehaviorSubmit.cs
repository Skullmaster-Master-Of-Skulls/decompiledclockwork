using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB6 RID: 3510
	[InterfaceType(1)]
	[Guid("3050F646-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorSubmit
	{
		// Token: 0x060174C1 RID: 95425
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSubmitInfo([MarshalAs(UnmanagedType.Interface)] [In] IHTMLSubmitData pSubmitData);

		// Token: 0x060174C2 RID: 95426
		[MethodImpl(MethodImplOptions.InternalCall)]
		void reset();
	}
}
