using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F4 RID: 500
	internal sealed class PlusNode : InteriorNode
	{
		// Token: 0x060020BC RID: 8380 RVA: 0x000B304C File Offset: 0x000B124C
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000B3086 File Offset: 0x000B1286
		public override bool IsNullable
		{
			get
			{
				return base.LeftChild.IsNullable;
			}
		}
	}
}
