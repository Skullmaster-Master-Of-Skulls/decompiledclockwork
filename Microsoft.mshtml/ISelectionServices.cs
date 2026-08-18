using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCA RID: 3274
	[InterfaceType(1)]
	[Guid("3050F684-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ISelectionServices
	{
		// Token: 0x0601630D RID: 90893
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetSelectionType([In] _SELECTION_TYPE eType, [MarshalAs(UnmanagedType.Interface)] [In] ISelectionServicesListener pIListener);

		// Token: 0x0601630E RID: 90894
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMarkupContainer([MarshalAs(UnmanagedType.Interface)] out IMarkupContainer ppIContainer);

		// Token: 0x0601630F RID: 90895
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddSegment([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEnd, [MarshalAs(UnmanagedType.Interface)] out ISegment ppISegmentAdded);

		// Token: 0x06016310 RID: 90896
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddElementSegment([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pIElement, [MarshalAs(UnmanagedType.Interface)] out IElementSegment ppISegmentAdded);

		// Token: 0x06016311 RID: 90897
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveSegment([MarshalAs(UnmanagedType.Interface)] [In] ISegment pISegment);

		// Token: 0x06016312 RID: 90898
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSelectionServicesListener([MarshalAs(UnmanagedType.Interface)] out ISelectionServicesListener ppISelectionServicesListener);
	}
}
