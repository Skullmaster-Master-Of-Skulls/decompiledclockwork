using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x0200039C RID: 924
	internal abstract class TreePrinter
	{
		// Token: 0x06003332 RID: 13106 RVA: 0x000C75DC File Offset: 0x000C57DC
		internal virtual string Print(TreeNode node)
		{
			this.PreProcess(node);
			StringBuilder stringBuilder = new StringBuilder();
			this.PrintNode(stringBuilder, node);
			return stringBuilder.ToString();
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000C7604 File Offset: 0x000C5804
		internal TreePrinter()
		{
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void PreProcess(TreeNode node)
		{
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void AfterAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void BeforeAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000C762E File Offset: 0x000C582E
		internal virtual void PrintNode(StringBuilder text, TreeNode node)
		{
			this.IndentLine(text);
			this.BeforeAppend(node, text);
			text.Append(node.Text.ToString());
			this.AfterAppend(node, text);
			this.PrintChildren(text, node);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000C7664 File Offset: 0x000C5864
		internal virtual void PrintChildren(StringBuilder text, TreeNode node)
		{
			this._scopes.Add(node);
			node.Position = 0;
			foreach (TreeNode node2 in node.Children)
			{
				text.AppendLine();
				int position = node.Position;
				node.Position = position + 1;
				this.PrintNode(text, node2);
			}
			this._scopes.RemoveAt(this._scopes.Count - 1);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000C76F4 File Offset: 0x000C58F4
		private void IndentLine(StringBuilder text)
		{
			int num = 0;
			for (int i = 0; i < this._scopes.Count; i++)
			{
				TreeNode treeNode = this._scopes[i];
				if (!this._showLines || (treeNode.Position == treeNode.Children.Count && i != this._scopes.Count - 1))
				{
					text.Append(' ');
				}
				else
				{
					text.Append(this._verticals);
				}
				num++;
				if (this._scopes.Count == num && this._showLines)
				{
					text.Append(this._horizontals);
				}
				else
				{
					text.Append(' ');
				}
			}
		}

		// Token: 0x0400166F RID: 5743
		private List<TreeNode> _scopes = new List<TreeNode>();

		// Token: 0x04001670 RID: 5744
		private bool _showLines = true;

		// Token: 0x04001671 RID: 5745
		private char _horizontals = '_';

		// Token: 0x04001672 RID: 5746
		private char _verticals = '|';
	}
}
