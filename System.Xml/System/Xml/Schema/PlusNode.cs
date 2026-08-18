using System;

namespace System.Xml.Schema
{
	// Token: 0x0200019C RID: 412
	internal sealed class PlusNode : InteriorNode
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x0005F210 File Offset: 0x0005E210
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0005F24A File Offset: 0x0005E24A
		public override bool IsNullable
		{
			get
			{
				return base.LeftChild.IsNullable;
			}
		}
	}
}
