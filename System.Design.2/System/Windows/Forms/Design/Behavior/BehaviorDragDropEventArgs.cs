using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000373 RID: 883
	public class BehaviorDragDropEventArgs : EventArgs
	{
		// Token: 0x0600242B RID: 9259 RVA: 0x000E0A98 File Offset: 0x000DEC98
		public BehaviorDragDropEventArgs(ICollection dragComponents)
		{
			this.dragComponents = dragComponents;
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000E0AA7 File Offset: 0x000DECA7
		public ICollection DragComponents
		{
			get
			{
				return this.dragComponents;
			}
		}

		// Token: 0x04001A50 RID: 6736
		private ICollection dragComponents;
	}
}
