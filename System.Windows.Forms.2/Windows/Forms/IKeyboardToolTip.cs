using System;
using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000291 RID: 657
	internal interface IKeyboardToolTip
	{
		// Token: 0x060029AD RID: 10669
		bool CanShowToolTipsNow();

		// Token: 0x060029AE RID: 10670
		Rectangle GetNativeScreenRectangle();

		// Token: 0x060029AF RID: 10671
		IList<Rectangle> GetNeighboringToolsRectangles();

		// Token: 0x060029B0 RID: 10672
		bool IsHoveredWithMouse();

		// Token: 0x060029B1 RID: 10673
		bool HasRtlModeEnabled();

		// Token: 0x060029B2 RID: 10674
		bool AllowsToolTip();

		// Token: 0x060029B3 RID: 10675
		IWin32Window GetOwnerWindow();

		// Token: 0x060029B4 RID: 10676
		void OnHooked(ToolTip toolTip);

		// Token: 0x060029B5 RID: 10677
		void OnUnhooked(ToolTip toolTip);

		// Token: 0x060029B6 RID: 10678
		string GetCaptionForTool(ToolTip toolTip);

		// Token: 0x060029B7 RID: 10679
		bool ShowsOwnToolTip();

		// Token: 0x060029B8 RID: 10680
		bool IsBeingTabbedTo();

		// Token: 0x060029B9 RID: 10681
		bool AllowsChildrenToShowToolTips();
	}
}
