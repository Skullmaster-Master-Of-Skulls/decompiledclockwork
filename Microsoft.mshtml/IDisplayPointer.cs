using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB0 RID: 3248
	[Guid("3050F69E-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IDisplayPointer
	{
		// Token: 0x0601626F RID: 90735
		[MethodImpl(MethodImplOptions.InternalCall)]
		void moveToPoint([In] tagPOINT ptPoint, [In] _COORD_SYSTEM eCoordSystem, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pElementContext, [In] uint dwHitTestOptions, out uint pdwHitTestResults);

		// Token: 0x06016270 RID: 90736
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveUnit([In] _DISPLAY_MOVEUNIT eMoveUnit, [In] int lXPos);

		// Token: 0x06016271 RID: 90737
		[MethodImpl(MethodImplOptions.InternalCall)]
		void PositionMarkupPointer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pMarkupPointer);

		// Token: 0x06016272 RID: 90738
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToPointer([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointer);

		// Token: 0x06016273 RID: 90739
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPointerGravity([In] _POINTER_GRAVITY eGravity);

		// Token: 0x06016274 RID: 90740
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPointerGravity(out _POINTER_GRAVITY peGravity);

		// Token: 0x06016275 RID: 90741
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetDisplayGravity([In] _DISPLAY_GRAVITY eGravity);

		// Token: 0x06016276 RID: 90742
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDisplayGravity(out _DISPLAY_GRAVITY peGravity);

		// Token: 0x06016277 RID: 90743
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsPositioned(out int pfPositioned);

		// Token: 0x06016278 RID: 90744
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Unposition();

		// Token: 0x06016279 RID: 90745
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsEqualTo([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointer, out int pfIsEqual);

		// Token: 0x0601627A RID: 90746
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsLeftOf([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointer, out int pfIsLeftOf);

		// Token: 0x0601627B RID: 90747
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsRightOf([MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispPointer, out int pfIsRightOf);

		// Token: 0x0601627C RID: 90748
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsAtBOL(out int pfBOL);

		// Token: 0x0601627D RID: 90749
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MoveToMarkupPointer([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pPointer, [MarshalAs(UnmanagedType.Interface)] [In] IDisplayPointer pDispLineContext);

		// Token: 0x0601627E RID: 90750
		[MethodImpl(MethodImplOptions.InternalCall)]
		void scrollIntoView();

		// Token: 0x0601627F RID: 90751
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetLineInfo([MarshalAs(UnmanagedType.Interface)] out ILineInfo ppLineInfo);

		// Token: 0x06016280 RID: 90752
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFlowElement([MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppLayoutElement);

		// Token: 0x06016281 RID: 90753
		[MethodImpl(MethodImplOptions.InternalCall)]
		void QueryBreaks(out uint pdwBreaks);
	}
}
