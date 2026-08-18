using System;

namespace System.Windows.Forms
{
	// Token: 0x02000411 RID: 1041
	public class TreeNodeMouseClickEventArgs : MouseEventArgs
	{
		// Token: 0x060048BF RID: 18623 RVA: 0x00132909 File Offset: 0x00130B09
		public TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y) : base(button, clicks, x, y, 0)
		{
			this.node = node;
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x0013291F File Offset: 0x00130B1F
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04002747 RID: 10055
		private TreeNode node;
	}
}
