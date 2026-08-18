using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000415 RID: 1045
	[ComVisible(true)]
	public class TreeNodeMouseHoverEventArgs : EventArgs
	{
		// Token: 0x060048F7 RID: 18679 RVA: 0x00133412 File Offset: 0x00131612
		public TreeNodeMouseHoverEventArgs(TreeNode node)
		{
			this.node = node;
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x00133421 File Offset: 0x00131621
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x0400274B RID: 10059
		private readonly TreeNode node;
	}
}
