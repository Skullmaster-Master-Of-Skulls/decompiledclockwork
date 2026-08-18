using System;

namespace System.Windows.Forms
{
	// Token: 0x0200030A RID: 778
	public class NodeLabelEditEventArgs : EventArgs
	{
		// Token: 0x06003170 RID: 12656 RVA: 0x000DF88E File Offset: 0x000DDA8E
		public NodeLabelEditEventArgs(TreeNode node)
		{
			this.node = node;
			this.label = null;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000DF8A4 File Offset: 0x000DDAA4
		public NodeLabelEditEventArgs(TreeNode node, string label)
		{
			this.node = node;
			this.label = label;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000DF8BA File Offset: 0x000DDABA
		// (set) Token: 0x06003173 RID: 12659 RVA: 0x000DF8C2 File Offset: 0x000DDAC2
		public bool CancelEdit
		{
			get
			{
				return this.cancelEdit;
			}
			set
			{
				this.cancelEdit = value;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000DF8CB File Offset: 0x000DDACB
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x000DF8D3 File Offset: 0x000DDAD3
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04001E2E RID: 7726
		private readonly string label;

		// Token: 0x04001E2F RID: 7727
		private readonly TreeNode node;

		// Token: 0x04001E30 RID: 7728
		private bool cancelEdit;
	}
}
