using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class CommonTree : BaseTree
	{
		// Token: 0x06000360 RID: 864 RVA: 0x00009003 File Offset: 0x00007203
		public CommonTree()
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009020 File Offset: 0x00007220
		public CommonTree(CommonTree node) : base(node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.Token = node.Token;
			this.startIndex = node.startIndex;
			this.stopIndex = node.stopIndex;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000907B File Offset: 0x0000727B
		public CommonTree(IToken t)
		{
			this.Token = t;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000909F File Offset: 0x0000729F
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000090DF File Offset: 0x000072DF
		public override int CharPositionInLine
		{
			get
			{
				if (this.Token != null && this.Token.CharPositionInLine != -1)
				{
					return this.Token.CharPositionInLine;
				}
				if (this.ChildCount > 0)
				{
					return this.Children[0].CharPositionInLine;
				}
				return 0;
			}
			set
			{
				base.CharPositionInLine = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000090E8 File Offset: 0x000072E8
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000090F0 File Offset: 0x000072F0
		public override int ChildIndex
		{
			get
			{
				return this.childIndex;
			}
			set
			{
				this.childIndex = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000090F9 File Offset: 0x000072F9
		public override bool IsNil
		{
			get
			{
				return this.Token == null;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00009104 File Offset: 0x00007304
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00009143 File Offset: 0x00007343
		public override int Line
		{
			get
			{
				if (this.Token != null && this.Token.Line != 0)
				{
					return this.Token.Line;
				}
				if (this.ChildCount > 0)
				{
					return this.Children[0].Line;
				}
				return 0;
			}
			set
			{
				base.Line = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000914C File Offset: 0x0000734C
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00009154 File Offset: 0x00007354
		public override ITree Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = (CommonTree)value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00009162 File Offset: 0x00007362
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00009179 File Offset: 0x00007379
		public override string Text
		{
			get
			{
				if (this.Token == null)
				{
					return null;
				}
				return this.Token.Text;
			}
			set
			{
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000917B File Offset: 0x0000737B
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00009183 File Offset: 0x00007383
		public IToken Token
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000918C File Offset: 0x0000738C
		// (set) Token: 0x06000371 RID: 881 RVA: 0x000091B1 File Offset: 0x000073B1
		public override int TokenStartIndex
		{
			get
			{
				if (this.startIndex == -1 && this.Token != null)
				{
					return this.Token.TokenIndex;
				}
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000091BA File Offset: 0x000073BA
		// (set) Token: 0x06000373 RID: 883 RVA: 0x000091DF File Offset: 0x000073DF
		public override int TokenStopIndex
		{
			get
			{
				if (this.stopIndex == -1 && this.Token != null)
				{
					return this.Token.TokenIndex;
				}
				return this.stopIndex;
			}
			set
			{
				this.stopIndex = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000091E8 File Offset: 0x000073E8
		// (set) Token: 0x06000375 RID: 885 RVA: 0x000091FF File Offset: 0x000073FF
		public override int Type
		{
			get
			{
				if (this.Token == null)
				{
					return 0;
				}
				return this.Token.Type;
			}
			set
			{
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009201 File Offset: 0x00007401
		public override ITree DupNode()
		{
			return new CommonTree(this);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000920C File Offset: 0x0000740C
		public virtual void SetUnknownTokenBoundaries()
		{
			if (this.Children == null)
			{
				if (this.startIndex < 0 || this.stopIndex < 0)
				{
					this.startIndex = (this.stopIndex = this.Token.TokenIndex);
				}
				return;
			}
			foreach (ITree tree in this.Children)
			{
				CommonTree commonTree = tree as CommonTree;
				if (commonTree != null)
				{
					commonTree.SetUnknownTokenBoundaries();
				}
			}
			if (this.startIndex >= 0 && this.stopIndex >= 0)
			{
				return;
			}
			if (this.Children.Count > 0)
			{
				ITree tree2 = this.Children[0];
				ITree tree3 = this.Children[this.Children.Count - 1];
				this.startIndex = tree2.TokenStartIndex;
				this.stopIndex = tree3.TokenStopIndex;
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009300 File Offset: 0x00007500
		public override string ToString()
		{
			if (this.IsNil)
			{
				return "nil";
			}
			if (this.Type == 0)
			{
				return "<errornode>";
			}
			if (this.Token == null)
			{
				return string.Empty;
			}
			return this.Token.Text;
		}

		// Token: 0x040000A4 RID: 164
		private IToken _token;

		// Token: 0x040000A5 RID: 165
		protected int startIndex = -1;

		// Token: 0x040000A6 RID: 166
		protected int stopIndex = -1;

		// Token: 0x040000A7 RID: 167
		private CommonTree parent;

		// Token: 0x040000A8 RID: 168
		private int childIndex = -1;
	}
}
