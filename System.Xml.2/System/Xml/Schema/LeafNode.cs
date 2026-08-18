using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EF RID: 495
	internal class LeafNode : SyntaxTreeNode
	{
		// Token: 0x0600209E RID: 8350 RVA: 0x000B2B0B File Offset: 0x000B0D0B
		public LeafNode(int pos)
		{
			this.pos = pos;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x000B2B1A File Offset: 0x000B0D1A
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x000B2B22 File Offset: 0x000B0D22
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

		// Token: 0x060020A1 RID: 8353 RVA: 0x000B2B2B File Offset: 0x000B0D2B
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000B2B2D File Offset: 0x000B0D2D
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafNode(positions.Add(positions[this.pos].symbol, positions[this.pos].particle));
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000B2B5C File Offset: 0x000B0D5C
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			firstpos.Set(this.pos);
			lastpos.Set(this.pos);
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x000B2B76 File Offset: 0x000B0D76
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000DBA RID: 3514
		private int pos;
	}
}
