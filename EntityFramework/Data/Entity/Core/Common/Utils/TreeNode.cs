using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000336 RID: 822
	internal class TreeNode
	{
		// Token: 0x06001C76 RID: 7286 RVA: 0x0008B537 File Offset: 0x00089737
		internal TreeNode()
		{
			this._text = new StringBuilder();
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0008B558 File Offset: 0x00089758
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

		// Token: 0x06001C78 RID: 7288 RVA: 0x0008B5A6 File Offset: 0x000897A6
		internal TreeNode(string text, List<TreeNode> children) : this(text, new TreeNode[0])
		{
			if (children != null)
			{
				this._children.AddRange(children);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0008B5C4 File Offset: 0x000897C4
		internal StringBuilder Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0008B5CC File Offset: 0x000897CC
		internal IList<TreeNode> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0008B5D4 File Offset: 0x000897D4
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0008B5DC File Offset: 0x000897DC
		internal int Position { get; set; }

		// Token: 0x040009D0 RID: 2512
		private readonly StringBuilder _text;

		// Token: 0x040009D1 RID: 2513
		private readonly List<TreeNode> _children = new List<TreeNode>();
	}
}
