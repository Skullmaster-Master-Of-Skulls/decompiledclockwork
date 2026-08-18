using System;

namespace System.Xml.Schema
{
	// Token: 0x0200019A RID: 410
	internal sealed class SequenceNode : InteriorNode
	{
		// Token: 0x06001567 RID: 5479 RVA: 0x0005F07C File Offset: 0x0005E07C
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(lastpos.Count);
			base.LeftChild.ConstructPos(firstpos, bitSet, followpos);
			BitSet bitSet2 = new BitSet(firstpos.Count);
			base.RightChild.ConstructPos(bitSet2, lastpos, followpos);
			if (base.LeftChild.IsNullable && !base.RightChild.IsRangeNode)
			{
				firstpos.Or(bitSet2);
			}
			if (base.RightChild.IsNullable)
			{
				lastpos.Or(bitSet);
			}
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				followpos[num].Or(bitSet2);
			}
			if (base.RightChild.IsRangeNode)
			{
				((LeafRangeNode)base.RightChild).NextIteration = firstpos.Clone();
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0005F134 File Offset: 0x0005E134
		public override bool IsNullable
		{
			get
			{
				return (base.LeftChild.IsNullable && (base.RightChild.IsNullable || base.RightChild.IsRangeNode)) || (base.RightChild.IsRangeNode && ((LeafRangeNode)base.RightChild).Min == 0m);
			}
		}
	}
}
