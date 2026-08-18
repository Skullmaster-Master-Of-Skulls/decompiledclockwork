using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB4 RID: 3508
	[Guid("3050F4EE-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementBehaviorSiteCategory
	{
		// Token: 0x060174BC RID: 95420
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumUnknown GetRelatedBehaviors([In] int lDirection, [MarshalAs(UnmanagedType.LPWStr)] [In] string pchCategory);
	}
}
