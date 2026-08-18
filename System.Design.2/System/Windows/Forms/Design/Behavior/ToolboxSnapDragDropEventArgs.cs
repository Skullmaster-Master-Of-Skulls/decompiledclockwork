using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000394 RID: 916
	internal sealed class ToolboxSnapDragDropEventArgs : DragEventArgs
	{
		// Token: 0x0600255B RID: 9563 RVA: 0x000EA26C File Offset: 0x000E846C
		public ToolboxSnapDragDropEventArgs(ToolboxSnapDragDropEventArgs.SnapDirection snapDirections, Point offset, DragEventArgs origArgs) : base(origArgs.Data, origArgs.KeyState, origArgs.X, origArgs.Y, origArgs.AllowedEffect, origArgs.Effect)
		{
			this.snapDirections = snapDirections;
			this.offset = offset;
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000EA2A6 File Offset: 0x000E84A6
		public ToolboxSnapDragDropEventArgs.SnapDirection SnapDirections
		{
			get
			{
				return this.snapDirections;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000EA2AE File Offset: 0x000E84AE
		public Point Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x04001B43 RID: 6979
		private ToolboxSnapDragDropEventArgs.SnapDirection snapDirections;

		// Token: 0x04001B44 RID: 6980
		private Point offset;

		// Token: 0x020005AC RID: 1452
		[Flags]
		public enum SnapDirection
		{
			// Token: 0x040022B1 RID: 8881
			None = 0,
			// Token: 0x040022B2 RID: 8882
			Top = 1,
			// Token: 0x040022B3 RID: 8883
			Bottom = 2,
			// Token: 0x040022B4 RID: 8884
			Right = 4,
			// Token: 0x040022B5 RID: 8885
			Left = 8
		}
	}
}
