using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000246 RID: 582
	public class DrawTreeNodeEventArgs : EventArgs
	{
		// Token: 0x06002504 RID: 9476 RVA: 0x000AD50D File Offset: 0x000AB70D
		public DrawTreeNodeEventArgs(Graphics graphics, TreeNode node, Rectangle bounds, TreeNodeStates state)
		{
			this.graphics = graphics;
			this.node = node;
			this.bounds = bounds;
			this.state = state;
			this.drawDefault = false;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x000AD539 File Offset: 0x000AB739
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x000AD541 File Offset: 0x000AB741
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
			set
			{
				this.drawDefault = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x000AD54A File Offset: 0x000AB74A
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x000AD552 File Offset: 0x000AB752
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000AD55A File Offset: 0x000AB75A
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000AD562 File Offset: 0x000AB762
		public TreeNodeStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04000F61 RID: 3937
		private readonly Graphics graphics;

		// Token: 0x04000F62 RID: 3938
		private readonly TreeNode node;

		// Token: 0x04000F63 RID: 3939
		private readonly Rectangle bounds;

		// Token: 0x04000F64 RID: 3940
		private readonly TreeNodeStates state;

		// Token: 0x04000F65 RID: 3941
		private bool drawDefault;
	}
}
