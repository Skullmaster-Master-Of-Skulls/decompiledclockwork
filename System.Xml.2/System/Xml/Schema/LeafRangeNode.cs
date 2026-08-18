using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F7 RID: 503
	internal sealed class LeafRangeNode : LeafNode
	{
		// Token: 0x060020C5 RID: 8389 RVA: 0x000B30FD File Offset: 0x000B12FD
		public LeafRangeNode(decimal min, decimal max) : this(-1, min, max)
		{
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000B3108 File Offset: 0x000B1308
		public LeafRangeNode(int pos, decimal min, decimal max) : base(pos)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x000B311F File Offset: 0x000B131F
		public decimal Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x000B3127 File Offset: 0x000B1327
		public decimal Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x000B312F File Offset: 0x000B132F
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x000B3137 File Offset: 0x000B1337
		public BitSet NextIteration
		{
			get
			{
				return this.nextIteration;
			}
			set
			{
				this.nextIteration = value;
			}
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000B3140 File Offset: 0x000B1340
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafRangeNode(base.Pos, this.min, this.max);
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x000B3159 File Offset: 0x000B1359
		public override bool IsRangeNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000B315C File Offset: 0x000B135C
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			if (parent.LeftChild.IsNullable)
			{
				this.min = 0m;
			}
		}

		// Token: 0x04000DBF RID: 3519
		private decimal min;

		// Token: 0x04000DC0 RID: 3520
		private decimal max;

		// Token: 0x04000DC1 RID: 3521
		private BitSet nextIteration;
	}
}
