using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCB RID: 3275
	[Guid("3050F662-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLEditDesigner
	{
		// Token: 0x06016313 RID: 90899
		[MethodImpl(MethodImplOptions.InternalCall)]
		void PreHandleEvent([In] int inEvtDispId, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pIEventObj);

		// Token: 0x06016314 RID: 90900
		[MethodImpl(MethodImplOptions.InternalCall)]
		void PostHandleEvent([In] int inEvtDispId, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pIEventObj);

		// Token: 0x06016315 RID: 90901
		[MethodImpl(MethodImplOptions.InternalCall)]
		void TranslateAccelerator([In] int inEvtDispId, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pIEventObj);

		// Token: 0x06016316 RID: 90902
		[MethodImpl(MethodImplOptions.InternalCall)]
		void PostEditorEventNotify([In] int inEvtDispId, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLEventObj pIEventObj);
	}
}
