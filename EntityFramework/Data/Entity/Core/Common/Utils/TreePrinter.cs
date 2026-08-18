using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x0200012A RID: 298
	internal abstract class TreePrinter
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00031E38 File Offset: 0x00030038
		internal virtual string Print(TreeNode node)
		{
			this.PreProcess(node);
			StringBuilder stringBuilder = new StringBuilder();
			this.PrintNode(stringBuilder, node);
			return stringBuilder.ToString();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00031E60 File Offset: 0x00030060
		internal virtual void PreProcess(TreeNode node)
		{
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00031E62 File Offset: 0x00030062
		internal virtual void AfterAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00031E64 File Offset: 0x00030064
		internal virtual void BeforeAppend(TreeNode node, StringBuilder text)
		{
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00031E66 File Offset: 0x00030066
		internal virtual void PrintNode(StringBuilder text, TreeNode node)
		{
			this.IndentLine(text);
			this.BeforeAppend(node, text);
			text.Append(node.Text);
			this.AfterAppend(node, text);
			this.PrintChildren(text, node);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00031E94 File Offset: 0x00030094
		internal virtual void PrintChildren(StringBuilder text, TreeNode node)
		{
			this._scopes.Add(node);
			node.Position = 0;
			foreach (TreeNode node2 in node.Children)
			{
				text.AppendLine();
				node.Position++;
				this.PrintNode(text, node2);
			}
			this._scopes.RemoveAt(this._scopes.Count - 1);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00031F24 File Offset: 0x00030124
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

		// Token: 0x04000298 RID: 664
		private readonly List<TreeNode> _scopes = new List<TreeNode>();

		// Token: 0x04000299 RID: 665
		private bool _showLines = true;

		// Token: 0x0400029A RID: 666
		private char _horizontals = '_';

		// Token: 0x0400029B RID: 667
		private char _verticals = '|';
	}
}
