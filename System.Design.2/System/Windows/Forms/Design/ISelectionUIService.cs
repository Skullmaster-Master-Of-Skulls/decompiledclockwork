using System;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002FF RID: 767
	internal interface ISelectionUIService
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001E76 RID: 7798
		// (set) Token: 0x06001E77 RID: 7799
		bool Visible { get; set; }

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06001E78 RID: 7800
		// (remove) Token: 0x06001E79 RID: 7801
		event ContainerSelectorActiveEventHandler ContainerSelectorActive;

		// Token: 0x06001E7A RID: 7802
		void AssignSelectionUIHandler(object component, ISelectionUIHandler handler);

		// Token: 0x06001E7B RID: 7803
		void ClearSelectionUIHandler(object component, ISelectionUIHandler handler);

		// Token: 0x06001E7C RID: 7804
		bool BeginDrag(SelectionRules rules, int initialX, int initialY);

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001E7D RID: 7805
		bool Dragging { get; }

		// Token: 0x06001E7E RID: 7806
		void DragMoved(Rectangle offset);

		// Token: 0x06001E7F RID: 7807
		void EndDrag(bool cancel);

		// Token: 0x06001E80 RID: 7808
		object[] FilterSelection(object[] components, SelectionRules selectionRules);

		// Token: 0x06001E81 RID: 7809
		Size GetAdornmentDimensions(AdornmentType adornmentType);

		// Token: 0x06001E82 RID: 7810
		bool GetAdornmentHitTest(object component, Point pt);

		// Token: 0x06001E83 RID: 7811
		bool GetContainerSelected(object component);

		// Token: 0x06001E84 RID: 7812
		SelectionRules GetSelectionRules(object component);

		// Token: 0x06001E85 RID: 7813
		SelectionStyles GetSelectionStyle(object component);

		// Token: 0x06001E86 RID: 7814
		void SetContainerSelected(object component, bool selected);

		// Token: 0x06001E87 RID: 7815
		void SetSelectionStyle(object component, SelectionStyles style);

		// Token: 0x06001E88 RID: 7816
		void SyncSelection();

		// Token: 0x06001E89 RID: 7817
		void SyncComponent(object component);
	}
}
