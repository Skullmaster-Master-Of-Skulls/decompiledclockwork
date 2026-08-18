using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F9 RID: 761
	internal interface IMouseHandler
	{
		// Token: 0x06001E4F RID: 7759
		void OnMouseDoubleClick(IComponent component);

		// Token: 0x06001E50 RID: 7760
		void OnMouseDown(IComponent component, MouseButtons button, int x, int y);

		// Token: 0x06001E51 RID: 7761
		void OnMouseHover(IComponent component);

		// Token: 0x06001E52 RID: 7762
		void OnMouseMove(IComponent component, int x, int y);

		// Token: 0x06001E53 RID: 7763
		void OnMouseUp(IComponent component, MouseButtons button);

		// Token: 0x06001E54 RID: 7764
		void OnSetCursor(IComponent component);
	}
}
