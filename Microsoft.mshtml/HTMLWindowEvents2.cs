using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B2 RID: 1970
	[InterfaceType(2)]
	[Guid("3050F625-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLWindowEvents2
	{
		// Token: 0x0600D64A RID: 54858
		[DispId(1003)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onload([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64B RID: 54859
		[DispId(1008)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onunload([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64C RID: 54860
		[DispId(-2147418102)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onhelp([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64D RID: 54861
		[DispId(-2147418111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onfocus([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64E RID: 54862
		[DispId(-2147418112)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onblur([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64F RID: 54863
		[DispId(1002)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onerror([MarshalAs(UnmanagedType.BStr)] [In] string description, [MarshalAs(UnmanagedType.BStr)] [In] string url, [In] int line);

		// Token: 0x0600D650 RID: 54864
		[DispId(1016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onresize([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D651 RID: 54865
		[DispId(1014)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onscroll([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D652 RID: 54866
		[DispId(1017)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onbeforeunload([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D653 RID: 54867
		[DispId(1024)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onbeforeprint([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D654 RID: 54868
		[DispId(1025)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onafterprint([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pEvtObj);
	}
}
