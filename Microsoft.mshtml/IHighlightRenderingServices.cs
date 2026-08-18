using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB4 RID: 3252
	[Guid("3050F606-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHighlightRenderingServices
	{
		// Token: 0x0601628C RID: 90764
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddSegment([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointerEnd, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLRenderStyle pIRenderStyle, [MarshalAs(UnmanagedType.Interface)] out IHighlightSegment ppISegment);

		// Token: 0x0601628D RID: 90765
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveSegmentToPointers([MarshalAs(UnmanagedType.Interface)] [In] IHighlightSegment pISegment, [MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointerStart, [MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointerEnd);

		// Token: 0x0601628E RID: 90766
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveSegment([MarshalAs(UnmanagedType.Interface)] [In] IHighlightSegment pISegment);
	}
}
