using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB7 RID: 3511
	[InterfaceType(1)]
	[Guid("3050F6B6-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorFocus
	{
		// Token: 0x060174C3 RID: 95427
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFocusRect([In] ref tagRECT pRect);
	}
}
