using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCC RID: 3276
	[InterfaceType(1)]
	[Guid("3050F663-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLEditServices
	{
		// Token: 0x06016317 RID: 90903
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddDesigner([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x06016318 RID: 90904
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveDesigner([MarshalAs(UnmanagedType.Interface)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x06016319 RID: 90905
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSelectionServices([MarshalAs(UnmanagedType.Interface)] [In] IMarkupContainer pIContainer, [MarshalAs(UnmanagedType.Interface)] out ISelectionServices ppSelSvc);

		// Token: 0x0601631A RID: 90906
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToSelectionAnchor([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIStartAnchor);

		// Token: 0x0601631B RID: 90907
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToSelectionEnd([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIEndAnchor);

		// Token: 0x0601631C RID: 90908
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SelectRange([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pEnd, [In] _SELECTION_TYPE eType);
	}
}
