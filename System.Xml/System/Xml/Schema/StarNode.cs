using System;

namespace System.Xml.Schema
{
	// Token: 0x0200019E RID: 414
	internal sealed class StarNode : InteriorNode
	{
		// Token: 0x06001573 RID: 5491 RVA: 0x0005F27C File Offset: 0x0005E27C
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0005F2B6 File Offset: 0x0005E2B6
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
