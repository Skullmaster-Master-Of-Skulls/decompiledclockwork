using System;

namespace System.Xml.Schema
{
	// Token: 0x0200019B RID: 411
	internal sealed class ChoiceNode : InteriorNode
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x0005F19C File Offset: 0x0005E19C
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			base.RightChild.ConstructPos(bitSet, bitSet2, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0005F1EB File Offset: 0x0005E1EB
		public override bool IsNullable
		{
			get
			{
				return base.LeftChild.IsNullable || base.RightChild.IsNullable;
			}
		}
	}
}
