using System;

namespace System.Windows.Forms
{
	// Token: 0x0200041D RID: 1053
	public class TreeViewEventArgs : EventArgs
	{
		// Token: 0x060049CC RID: 18892 RVA: 0x00136FDB File Offset: 0x001351DB
		public TreeViewEventArgs(TreeNode node)
		{
			this.node = node;
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x00136FEA File Offset: 0x001351EA
		public TreeViewEventArgs(TreeNode node, TreeViewAction action)
		{
			this.node = node;
			this.action = action;
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x00137000 File Offset: 0x00135200
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x00137008 File Offset: 0x00135208
		public TreeViewAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x040027A3 RID: 10147
		private TreeNode node;

		// Token: 0x040027A4 RID: 10148
		private TreeViewAction action;
	}
}
