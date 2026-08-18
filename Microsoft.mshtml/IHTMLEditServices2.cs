using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCD RID: 3277
	[Guid("3050F812-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLEditServices2 : IHTMLEditServices
	{
		// Token: 0x0601631D RID: 90909
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddDesigner([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x0601631E RID: 90910
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveDesigner([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x0601631F RID: 90911
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSelectionServices([MarshalAs(UnmanagedType.Interface)] [In] IMarkupContainer pIContainer, [MarshalAs(UnmanagedType.Interface)] out ISelectionServices ppSelSvc);

		// Token: 0x06016320 RID: 90912
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToSelectionAnchor([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIStartAnchor);

		// Token: 0x06016321 RID: 90913
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToSelectionEnd([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEndAnchor);

		// Token: 0x06016322 RID: 90914
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SelectRange([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pEnd, [In] _SELECTION_TYPE eType);

		// Token: 0x06016323 RID: 90915
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToSelectionAnchorEx([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pIStartAnchor);

		// Token: 0x06016324 RID: 90916
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToSelectionEndEx([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pIEndAnchor);

		// Token: 0x06016325 RID: 90917
		[MethodImpl(MethodImplOptions.InternalCall)]
		void FreezeVirtualCaretPos([In] int fReCompute);

		// Token: 0x06016326 RID: 90918
		[MethodImpl(MethodImplOptions.InternalCall)]
		void UnFreezeVirtualCaretPos([In] int fReset);
	}
}
