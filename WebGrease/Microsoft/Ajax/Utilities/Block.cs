using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000068 RID: 104
	public sealed class Block : AstNode, IEnumerable<AstNode>, IEnumerable
	{
		// Token: 0x17000187 RID: 391
		public AstNode this[int index]
		{
			get
			{
				return this.m_list[index];
			}
			set
			{
				this.m_list[index].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				if (value != null)
				{
					this.m_list[index] = value;
					this.m_list[index].Parent = this;
					return;
				}
				this.m_list.RemoveAt(index);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00021CCA File Offset: 0x0001FECA
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00021CD7 File Offset: 0x0001FED7
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00021CDF File Offset: 0x0001FEDF
		public bool BraceOnNewLine { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		public bool IsModule { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00021CF9 File Offset: 0x0001FEF9
		public override Context TerminatingContext
		{
			get
			{
				Context terminatingContext;
				if ((terminatingContext = base.TerminatingContext) == null)
				{
					if (this.m_list.Count != 1)
					{
						return null;
					}
					terminatingContext = this.m_list[0].TerminatingContext;
				}
				return terminatingContext;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00021D26 File Offset: 0x0001FF26
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x00021D2E File Offset: 0x0001FF2E
		public bool ForceBraces { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00021D37 File Offset: 0x0001FF37
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00021D3F File Offset: 0x0001FF3F
		public bool IsConcise { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00021D48 File Offset: 0x0001FF48
		public override bool IsExpression
		{
			get
			{
				return this.m_list.Count == 1 && this.m_list[0].IsExpression;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00021D6B File Offset: 0x0001FF6B
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes<AstNode>(this.m_list);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00021D78 File Offset: 0x0001FF78
		public Block(Context context) : base(context)
		{
			this.m_list = new List<AstNode>();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00021D8C File Offset: 0x0001FF8C
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00021DC0 File Offset: 0x0001FFC0
		public void Clear()
		{
			foreach (AstNode obj in this.m_list)
			{
				obj.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
			}
			this.m_list.Clear();
			this.IsConcise = false;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00021E38 File Offset: 0x00020038
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return this.m_list.Count == 1 && this.m_list[0].EncloseBlock(type);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00021E84 File Offset: 0x00020084
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			for (int i = this.m_list.Count - 1; i >= 0; i--)
			{
				if (this.m_list[i] == oldNode)
				{
					this.m_list[i].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
					if (newNode == null)
					{
						this.IsConcise = false;
						this.m_list.RemoveAt(i);
						this.IsConcise = false;
					}
					else
					{
						Block block = newNode as Block;
						if (block != null)
						{
							this.m_list.RemoveAt(i);
							this.InsertRange(i, block.m_list);
						}
						else
						{
							this.m_list[i] = newNode;
							newNode.Parent = this;
							if (this.IsConcise && !newNode.IsExpression)
							{
								this.IsConcise = false;
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00021F58 File Offset: 0x00020158
		public void Append(AstNode item)
		{
			if (item != null)
			{
				if (this.IsConcise)
				{
					this.Unconcise();
				}
				item.Parent = this;
				this.m_list.Add(item);
				base.Context.UpdateWith(item.Context);
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00021F90 File Offset: 0x00020190
		public int IndexOf(AstNode item)
		{
			return this.m_list.IndexOf(item);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00021FA0 File Offset: 0x000201A0
		public void InsertAfter(AstNode after, AstNode item)
		{
			if (item != null)
			{
				int num = this.m_list.IndexOf(after);
				if (num >= 0)
				{
					if (this.IsConcise)
					{
						this.Unconcise();
					}
					Block block = item as Block;
					if (block != null)
					{
						this.InsertRange(num + 1, block.Children);
						return;
					}
					item.Parent = this;
					this.m_list.Insert(num + 1, item);
				}
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00022000 File Offset: 0x00020200
		public void Insert(int index, AstNode item)
		{
			if (item != null)
			{
				if (this.IsConcise)
				{
					this.Unconcise();
				}
				Block block = item as Block;
				if (block != null)
				{
					this.InsertRange(index, block.Children);
					return;
				}
				item.Parent = this;
				this.m_list.Insert(index, item);
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0002204A File Offset: 0x0002024A
		public void RemoveLast()
		{
			this.IsConcise = false;
			this.RemoveAt(this.m_list.Count - 1);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00022090 File Offset: 0x00020290
		public void RemoveAt(int index)
		{
			if (0 <= index && index < this.m_list.Count)
			{
				this.IsConcise = false;
				this.m_list[index].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_list.RemoveAt(index);
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000220E8 File Offset: 0x000202E8
		public void InsertRange(int index, IEnumerable<AstNode> newItems)
		{
			if (newItems != null)
			{
				if (this.IsConcise)
				{
					this.Unconcise();
				}
				this.m_list.InsertRange(index, newItems);
				foreach (AstNode astNode in newItems)
				{
					astNode.Parent = this;
				}
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00022150 File Offset: 0x00020350
		private void Unconcise()
		{
			this.IsConcise = false;
			if (this.m_list.Count == 1)
			{
				AstNode astNode = this.m_list[0];
				if (astNode.IsExpression)
				{
					this.m_list[0] = new ReturnNode(astNode.Context)
					{
						Operand = astNode,
						Parent = this
					};
				}
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000221AE File Offset: 0x000203AE
		public IEnumerator<AstNode> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000221C0 File Offset: 0x000203C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x0400026C RID: 620
		private List<AstNode> m_list;
	}
}
