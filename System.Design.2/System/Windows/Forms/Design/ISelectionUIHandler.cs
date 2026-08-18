using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002FE RID: 766
	internal interface ISelectionUIHandler
	{
		// Token: 0x06001E69 RID: 7785
		bool BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY);

		// Token: 0x06001E6A RID: 7786
		void DragMoved(object[] components, Rectangle offset);

		// Token: 0x06001E6B RID: 7787
		void EndDrag(object[] components, bool cancel);

		// Token: 0x06001E6C RID: 7788
		Rectangle GetComponentBounds(object component);

		// Token: 0x06001E6D RID: 7789
		SelectionRules GetComponentRules(object component);

		// Token: 0x06001E6E RID: 7790
		Rectangle GetSelectionClipRect(object component);

		// Token: 0x06001E6F RID: 7791
		void OnSelectionDoubleClick(IComponent component);

		// Token: 0x06001E70 RID: 7792
		bool QueryBeginDrag(object[] components, SelectionRules rules, int initialX, int initialY);

		// Token: 0x06001E71 RID: 7793
		void ShowContextMenu(IComponent component);

		// Token: 0x06001E72 RID: 7794
		void OleDragEnter(DragEventArgs de);

		// Token: 0x06001E73 RID: 7795
		void OleDragDrop(DragEventArgs de);

		// Token: 0x06001E74 RID: 7796
		void OleDragOver(DragEventArgs de);

		// Token: 0x06001E75 RID: 7797
		void OleDragLeave();
	}
}
