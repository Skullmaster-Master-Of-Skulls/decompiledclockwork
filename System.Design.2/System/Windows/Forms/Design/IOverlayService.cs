using System;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002FD RID: 765
	internal interface IOverlayService
	{
		// Token: 0x06001E64 RID: 7780
		int PushOverlay(Control control);

		// Token: 0x06001E65 RID: 7781
		void RemoveOverlay(Control control);

		// Token: 0x06001E66 RID: 7782
		void InsertOverlay(Control control, int index);

		// Token: 0x06001E67 RID: 7783
		void InvalidateOverlays(Rectangle screenRectangle);

		// Token: 0x06001E68 RID: 7784
		void InvalidateOverlays(Region screenRegion);
	}
}
