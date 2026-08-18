using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB2 RID: 3506
	[Guid("3050F659-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementBehaviorSiteOM2 : IElementBehaviorSiteOM
	{
		// Token: 0x060174B4 RID: 95412
		[MethodImpl(MethodImplOptions.InternalCall)]
		int RegisterEvent([MarshalAs(UnmanagedType.LPWStr)] [In] string pchEvent, [In] int lFlags);

		// Token: 0x060174B5 RID: 95413
		[MethodImpl(MethodImplOptions.InternalCall)]
		int GetEventCookie([MarshalAs(UnmanagedType.LPWStr)] [In] string pchEvent);

		// Token: 0x060174B6 RID: 95414
		[MethodImpl(MethodImplOptions.InternalCall)]
		void FireEvent([In] int lCookie, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEventObject);

		// Token: 0x060174B7 RID: 95415
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLEventObj CreateEventObject();

		// Token: 0x060174B8 RID: 95416
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterName([MarshalAs(UnmanagedType.LPWStr)] [In] string pchName);

		// Token: 0x060174B9 RID: 95417
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterUrn([MarshalAs(UnmanagedType.LPWStr)] [In] string pchUrn);

		// Token: 0x060174BA RID: 95418
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElementDefaults GetDefaults();
	}
}
