using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200003F RID: 63
	[DebuggerTypeProxy(typeof(AntlrRuntime_BaseTreeDebugView))]
	[Serializable]
	public abstract class BaseTree : ITree
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00007B19 File Offset: 0x00005D19
		public BaseTree()
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007B21 File Offset: 0x00005D21
		public BaseTree(ITree node)
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00007B29 File Offset: 0x00005D29
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00007B31 File Offset: 0x00005D31
		public virtual IList<ITree> Children
		{
			get
			{
				return this._children;
			}
			private set
			{
				this._children = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00007B3A File Offset: 0x00005D3A
		public virtual int ChildCount
		{
			get
			{
				if (this.Children == null)
				{
					return 0;
				}
				return this.Children.Count;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00007B51 File Offset: 0x00005D51
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00007B54 File Offset: 0x00005D54
		public virtual ITree Parent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00007B56 File Offset: 0x00005D56
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00007B59 File Offset: 0x00005D59
		public virtual int ChildIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007B5B File Offset: 0x00005D5B
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002BD RID: 701
		// (set) Token: 0x060002BE RID: 702
		public abstract int TokenStartIndex { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002BF RID: 703
		// (set) Token: 0x060002C0 RID: 704
		public abstract int TokenStopIndex { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002C1 RID: 705
		// (set) Token: 0x060002C2 RID: 706
		public abstract int Type { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002C3 RID: 707
		// (set) Token: 0x060002C4 RID: 708
		public abstract string Text { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00007B5E File Offset: 0x00005D5E
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00007B66 File Offset: 0x00005D66
		public virtual int Line { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00007B6F File Offset: 0x00005D6F
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00007B77 File Offset: 0x00005D77
		public virtual int CharPositionInLine { get; set; }

		// Token: 0x060002C9 RID: 713 RVA: 0x00007B80 File Offset: 0x00005D80
		public virtual ITree GetChild(int i)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.Children == null || i >= this.Children.Count)
			{
				return null;
			}
			return this.Children[i];
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public virtual ITree GetFirstChildWithType(int type)
		{
			foreach (ITree tree in this.Children)
			{
				if (tree.Type == type)
				{
					return tree;
				}
			}
			return null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007C08 File Offset: 0x00005E08
		public virtual void AddChild(ITree t)
		{
			if (t == null)
			{
				return;
			}
			if (t.IsNil)
			{
				BaseTree baseTree = t as BaseTree;
				if (baseTree != null && this.Children != null && this.Children == baseTree.Children)
				{
					throw new Exception("attempt to add child list to itself");
				}
				if (t.ChildCount > 0)
				{
					if (this.Children != null || baseTree == null)
					{
						if (this.Children == null)
						{
							this.Children = this.CreateChildrenList();
						}
						int childCount = t.ChildCount;
						for (int i = 0; i < childCount; i++)
						{
							ITree child = t.GetChild(i);
							this.Children.Add(child);
							child.Parent = this;
							child.ChildIndex = this.Children.Count - 1;
						}
						return;
					}
					this.Children = baseTree.Children;
					this.FreshenParentAndChildIndexes();
					return;
				}
			}
			else
			{
				if (this.Children == null)
				{
					this.Children = this.CreateChildrenList();
				}
				this.Children.Add(t);
				t.Parent = this;
				t.ChildIndex = this.Children.Count - 1;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007D0C File Offset: 0x00005F0C
		public virtual void AddChildren(IEnumerable<ITree> kids)
		{
			if (kids == null)
			{
				throw new ArgumentNullException("kids");
			}
			foreach (ITree t in kids)
			{
				this.AddChild(t);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007D64 File Offset: 0x00005F64
		public virtual void SetChild(int i, ITree t)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (t == null)
			{
				return;
			}
			if (t.IsNil)
			{
				throw new ArgumentException("Can't set single child to a list");
			}
			if (this.Children == null)
			{
				this.Children = this.CreateChildrenList();
			}
			this.Children[i] = t;
			t.Parent = this;
			t.ChildIndex = i;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public virtual void InsertChild(int i, ITree t)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (i > this.ChildCount)
			{
				throw new ArgumentException();
			}
			if (i == this.ChildCount)
			{
				this.AddChild(t);
				return;
			}
			this.Children.Insert(i, t);
			this.FreshenParentAndChildIndexes(i);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007E18 File Offset: 0x00006018
		public virtual object DeleteChild(int i)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (i >= this.ChildCount)
			{
				throw new ArgumentException();
			}
			if (this.Children == null)
			{
				return null;
			}
			ITree result = this.Children[i];
			this.Children.RemoveAt(i);
			this.FreshenParentAndChildIndexes(i);
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007E70 File Offset: 0x00006070
		public virtual void ReplaceChildren(int startChildIndex, int stopChildIndex, object t)
		{
			if (startChildIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (stopChildIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (stopChildIndex < startChildIndex)
			{
				throw new ArgumentException();
			}
			if (this.Children == null)
			{
				throw new ArgumentException("indexes invalid; no children in list");
			}
			int num = stopChildIndex - startChildIndex + 1;
			ITree tree = (ITree)t;
			IList<ITree> list;
			if (tree.IsNil)
			{
				BaseTree baseTree = tree as BaseTree;
				if (baseTree != null && baseTree.Children != null)
				{
					list = baseTree.Children;
				}
				else
				{
					list = this.CreateChildrenList();
					int childCount = tree.ChildCount;
					for (int i = 0; i < childCount; i++)
					{
						list.Add(tree.GetChild(i));
					}
				}
			}
			else
			{
				list = new List<ITree>(1);
				list.Add(tree);
			}
			int count = list.Count;
			int count2 = list.Count;
			int num2 = num - count;
			if (num2 == 0)
			{
				int num3 = 0;
				for (int j = startChildIndex; j <= stopChildIndex; j++)
				{
					ITree tree2 = list[num3];
					this.Children[j] = tree2;
					tree2.Parent = this;
					tree2.ChildIndex = j;
					num3++;
				}
				return;
			}
			if (num2 > 0)
			{
				for (int k = 0; k < count2; k++)
				{
					this.Children[startChildIndex + k] = list[k];
				}
				int num4 = startChildIndex + count2;
				for (int l = num4; l <= stopChildIndex; l++)
				{
					this.Children.RemoveAt(num4);
				}
				this.FreshenParentAndChildIndexes(startChildIndex);
				return;
			}
			for (int m = 0; m < num; m++)
			{
				this.Children[startChildIndex + m] = list[m];
			}
			for (int n = num; n < count; n++)
			{
				this.Children.Insert(startChildIndex + n, list[n]);
			}
			this.FreshenParentAndChildIndexes(startChildIndex);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000803A File Offset: 0x0000623A
		protected virtual IList<ITree> CreateChildrenList()
		{
			return new List<ITree>();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008041 File Offset: 0x00006241
		public virtual void FreshenParentAndChildIndexes()
		{
			this.FreshenParentAndChildIndexes(0);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000804C File Offset: 0x0000624C
		public virtual void FreshenParentAndChildIndexes(int offset)
		{
			int childCount = this.ChildCount;
			for (int i = offset; i < childCount; i++)
			{
				ITree child = this.GetChild(i);
				child.ChildIndex = i;
				child.Parent = this;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008082 File Offset: 0x00006282
		public virtual void FreshenParentAndChildIndexesDeeply()
		{
			this.FreshenParentAndChildIndexesDeeply(0);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000808C File Offset: 0x0000628C
		public virtual void FreshenParentAndChildIndexesDeeply(int offset)
		{
			int childCount = this.ChildCount;
			for (int i = offset; i < childCount; i++)
			{
				ITree child = this.GetChild(i);
				child.ChildIndex = i;
				child.Parent = this;
				BaseTree baseTree = child as BaseTree;
				if (baseTree != null)
				{
					baseTree.FreshenParentAndChildIndexesDeeply();
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000080D2 File Offset: 0x000062D2
		public virtual void SanityCheckParentAndChildIndexes()
		{
			this.SanityCheckParentAndChildIndexes(null, -1);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000080DC File Offset: 0x000062DC
		public virtual void SanityCheckParentAndChildIndexes(ITree parent, int i)
		{
			if (parent != this.Parent)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"parents don't match; expected ",
					parent,
					" found ",
					this.Parent
				}));
			}
			if (i != this.ChildIndex)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"child indexes don't match; expected ",
					i,
					" found ",
					this.ChildIndex
				}));
			}
			int childCount = this.ChildCount;
			for (int j = 0; j < childCount; j++)
			{
				BaseTree baseTree = (BaseTree)this.GetChild(j);
				baseTree.SanityCheckParentAndChildIndexes(this, j);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008193 File Offset: 0x00006393
		public virtual bool HasAncestor(int ttype)
		{
			return this.GetAncestor(ttype) != null;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000081A4 File Offset: 0x000063A4
		public virtual ITree GetAncestor(int ttype)
		{
			for (ITree parent = ((ITree)this).Parent; parent != null; parent = parent.Parent)
			{
				if (parent.Type == ttype)
				{
					return parent;
				}
			}
			return null;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000081D4 File Offset: 0x000063D4
		public virtual IList<ITree> GetAncestors()
		{
			if (this.Parent == null)
			{
				return null;
			}
			List<ITree> list = new List<ITree>();
			for (ITree parent = ((ITree)this).Parent; parent != null; parent = parent.Parent)
			{
				list.Insert(0, parent);
			}
			return list;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008210 File Offset: 0x00006410
		public virtual string ToStringTree()
		{
			if (this.Children == null || this.Children.Count == 0)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsNil)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(this.ToString());
				stringBuilder.Append(' ');
			}
			int num = 0;
			while (this.Children != null && num < this.Children.Count)
			{
				ITree tree = this.Children[num];
				if (num > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(tree.ToStringTree());
				num++;
			}
			if (!this.IsNil)
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002DC RID: 732
		public abstract override string ToString();

		// Token: 0x060002DD RID: 733
		public abstract ITree DupNode();

		// Token: 0x0400008F RID: 143
		private IList<ITree> _children;
	}
}
