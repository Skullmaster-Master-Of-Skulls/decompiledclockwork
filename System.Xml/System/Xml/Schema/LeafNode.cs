using System;

namespace System.Xml.Schema
{
	// Token: 0x02000197 RID: 407
	internal class LeafNode : SyntaxTreeNode
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x0005EE83 File Offset: 0x0005DE83
		public LeafNode(int pos)
		{
			this.pos = pos;
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0005EE92 File Offset: 0x0005DE92
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0005EE9A File Offset: 0x0005DE9A
		public int Pos
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0005EEA3 File Offset: 0x0005DEA3
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0005EEA5 File Offset: 0x0005DEA5
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafNode(positions.Add(positions[this.pos].symbol, positions[this.pos].particle));
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0005EED4 File Offset: 0x0005DED4
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			firstpos.Set(this.pos);
			lastpos.Set(this.pos);
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0005EEEE File Offset: 0x0005DEEE
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000CC5 RID: 3269
		private int pos;
	}
}
