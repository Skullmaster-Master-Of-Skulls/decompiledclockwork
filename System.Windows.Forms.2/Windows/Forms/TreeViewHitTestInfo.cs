using System;

namespace System.Windows.Forms
{
	// Token: 0x0200041F RID: 1055
	public class TreeViewHitTestInfo
	{
		// Token: 0x060049D4 RID: 18900 RVA: 0x00137010 File Offset: 0x00135210
		public TreeViewHitTestInfo(TreeNode hitNode, TreeViewHitTestLocations hitLocation)
		{
			this.node = hitNode;
			this.loc = hitLocation;
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x00137026 File Offset: 0x00135226
		public TreeViewHitTestLocations Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x0013702E File Offset: 0x0013522E
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x040027A5 RID: 10149
		private TreeViewHitTestLocations loc;

		// Token: 0x040027A6 RID: 10150
		private TreeNode node;
	}
}
