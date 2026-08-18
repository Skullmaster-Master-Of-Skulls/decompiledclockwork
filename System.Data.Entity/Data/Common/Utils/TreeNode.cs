using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x0200039B RID: 923
	internal class TreeNode
	{
		// Token: 0x0600332B RID: 13099 RVA: 0x000C752E File Offset: 0x000C572E
		internal TreeNode()
		{
			this._text = new StringBuilder();
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000C754C File Offset: 0x000C574C
		internal TreeNode(string text, params TreeNode[] children)
		{
			if (string.IsNullOrEmpty(text))
			{
				this._text = new StringBuilder();
			}
			else
			{
				this._text = new StringBuilder(text);
			}
			if (children != null)
			{
				this._children.AddRange(children);
			}
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000C759A File Offset: 0x000C579A
		internal TreeNode(string text, List<TreeNode> children) : this(text, new TreeNode[0])
		{
			if (children != null)
			{
				this._children.AddRange(children);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000C75B8 File Offset: 0x000C57B8
		internal StringBuilder Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C75C0 File Offset: 0x000C57C0
		internal IList<TreeNode> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000C75C8 File Offset: 0x000C57C8
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x000C75D0 File Offset: 0x000C57D0
		internal int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x0400166C RID: 5740
		private StringBuilder _text;

		// Token: 0x0400166D RID: 5741
		private List<TreeNode> _children = new List<TreeNode>();

		// Token: 0x0400166E RID: 5742
		private int _position;
	}
}
