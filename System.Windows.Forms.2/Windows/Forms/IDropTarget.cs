using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A4 RID: 676
	public interface IDropTarget
	{
		// Token: 0x06002A33 RID: 10803
		void OnDragEnter(DragEventArgs e);

		// Token: 0x06002A34 RID: 10804
		void OnDragLeave(EventArgs e);

		// Token: 0x06002A35 RID: 10805
		void OnDragDrop(DragEventArgs e);

		// Token: 0x06002A36 RID: 10806
		void OnDragOver(DragEventArgs e);
	}
}
