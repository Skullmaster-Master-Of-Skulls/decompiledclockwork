using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB5 RID: 3253
	[InterfaceType(1)]
	[Guid("3050F69D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IDisplayServices
	{
		// Token: 0x0601628F RID: 90767
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateDisplayPointer([MarshalAs(UnmanagedType.Interface)] out IDisplayPointer ppDispPointer);

		// Token: 0x06016290 RID: 90768
		[MethodImpl(MethodImplOptions.InternalCall)]
		void TransformRect([In] [Out] ref tagRECT pRect, [In] _COORD_SYSTEM eSource, [In] _COORD_SYSTEM eDestination, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement);

		// Token: 0x06016291 RID: 90769
		[MethodImpl(MethodImplOptions.InternalCall)]
		void TransformPoint([In] [Out] ref tagPOINT pPoint, [In] _COORD_SYSTEM eSource, [In] _COORD_SYSTEM eDestination, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement);

		// Token: 0x06016292 RID: 90770
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCaret([MarshalAs(UnmanagedType.Interface)] out IHTMLCaret ppCaret);

		// Token: 0x06016293 RID: 90771
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetComputedStyle([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointer, [MarshalAs(UnmanagedType.Interface)] out IHTMLComputedStyle ppComputedStyle);

		// Token: 0x06016294 RID: 90772
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ScrollRectIntoView([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement, [In] tagRECT rect);

		// Token: 0x06016295 RID: 90773
		[MethodImpl(MethodImplOptions.InternalCall)]
		void HasFlowLayout([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement, out int pfHasFlowLayout);
	}
}
