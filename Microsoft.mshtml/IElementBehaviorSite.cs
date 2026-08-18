using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000006 RID: 6
	[InterfaceType(1)]
	[Guid("3050F427-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorSite
	{
		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElement GetElement();

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterNotification([In] int lEvent);
	}
}
