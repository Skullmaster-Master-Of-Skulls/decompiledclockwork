using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004FE RID: 1278
	public sealed class TreeNodeEventArgs : EventArgs
	{
		// Token: 0x06004019 RID: 16409 RVA: 0x000CF18D File Offset: 0x000CD38D
		public TreeNodeEventArgs(TreeNode node)
		{
			this._node = node;
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x000CF19C File Offset: 0x000CD39C
		public TreeNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0400246C RID: 9324
		private TreeNode _node;
	}
}
