using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000009 RID: 9
	[InterfaceType(1)]
	[Guid("3050F489-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorSiteOM
	{
		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		int RegisterEvent([MarshalAs(UnmanagedType.LPWStr)] [In] string pchEvent, [In] int lFlags);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		int GetEventCookie([MarshalAs(UnmanagedType.LPWStr)] [In] string pchEvent);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		void FireEvent([In] int lCookie, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEventObject);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLEventObj CreateEventObject();

		// Token: 0x06000131 RID: 305
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterName([MarshalAs(UnmanagedType.LPWStr)] [In] string pchName);

		// Token: 0x06000132 RID: 306
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterUrn([MarshalAs(UnmanagedType.LPWStr)] [In] string pchUrn);
	}
}
