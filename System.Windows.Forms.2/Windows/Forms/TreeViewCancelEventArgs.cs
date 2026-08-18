using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200041A RID: 1050
	public class TreeViewCancelEventArgs : CancelEventArgs
	{
		// Token: 0x060049C5 RID: 18885 RVA: 0x00136FB4 File Offset: 0x001351B4
		public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action) : base(cancel)
		{
			this.node = node;
			this.action = action;
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x00136FCB File Offset: 0x001351CB
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x00136FD3 File Offset: 0x001351D3
		public TreeViewAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400279D RID: 10141
		private TreeNode node;

		// Token: 0x0400279E RID: 10142
		private TreeViewAction action;
	}
}
