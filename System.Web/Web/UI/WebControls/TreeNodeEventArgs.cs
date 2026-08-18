using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200066F RID: 1647
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TreeNodeEventArgs : EventArgs
	{
		// Token: 0x060050D3 RID: 20691 RVA: 0x00144BCE File Offset: 0x00143BCE
		public TreeNodeEventArgs(TreeNode node)
		{
			this._node = node;
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x00144BDD File Offset: 0x00143BDD
		public TreeNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x04002D37 RID: 11575
		private TreeNode _node;
	}
}
