using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F6 RID: 502
	internal sealed class StarNode : InteriorNode
	{
		// Token: 0x060020C2 RID: 8386 RVA: 0x000B30B8 File Offset: 0x000B12B8
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x000B30F2 File Offset: 0x000B12F2
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
