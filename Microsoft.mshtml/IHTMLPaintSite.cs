using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE7 RID: 3303
	[ComConversionLoss]
	[Guid("3050F6A7-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLPaintSite
	{
		// Token: 0x06016359 RID: 90969
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InvalidatePainterInfo();

		// Token: 0x0601635A RID: 90970
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InvalidateRect([In] ref tagRECT prcInvalid);

		// Token: 0x0601635B RID: 90971
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InvalidateRegion([In] IntPtr rgnInvalid);

		// Token: 0x0601635C RID: 90972
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDrawInfo([In] int lFlags, out _HTML_PAINT_DRAW_INFO pDrawInfo);

		// Token: 0x0601635D RID: 90973
		[MethodImpl(MethodImplOptions.InternalCall)]
		void TransformGlobalToLocal([In] tagPOINT ptGlobal, out tagPOINT pptLocal);

		// Token: 0x0601635E RID: 90974
		[MethodImpl(MethodImplOptions.InternalCall)]
		void TransformLocalToGlobal([In] tagPOINT ptLocal, out tagPOINT pptGlobal);

		// Token: 0x0601635F RID: 90975
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetHitTestCookie(out int plCookie);
	}
}
