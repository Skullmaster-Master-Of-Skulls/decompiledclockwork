using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F3 RID: 499
	internal sealed class ChoiceNode : InteriorNode
	{
		// Token: 0x060020B7 RID: 8375 RVA: 0x000B2F68 File Offset: 0x000B1168
		private static void ConstructChildPos(SyntaxTreeNode child, BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			child.ConstructPos(bitSet, bitSet2, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000B2FA4 File Offset: 0x000B11A4
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			ChoiceNode choiceNode = this;
			SyntaxTreeNode leftChild;
			do
			{
				ChoiceNode.ConstructChildPos(choiceNode.RightChild, bitSet, bitSet2, followpos);
				leftChild = choiceNode.LeftChild;
				choiceNode = (leftChild as ChoiceNode);
			}
			while (choiceNode != null);
			leftChild.ConstructPos(firstpos, lastpos, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000B3004 File Offset: 0x000B1204
		public override bool IsNullable
		{
			get
			{
				ChoiceNode choiceNode = this;
				while (!choiceNode.RightChild.IsNullable)
				{
					SyntaxTreeNode leftChild = choiceNode.LeftChild;
					choiceNode = (leftChild as ChoiceNode);
					if (choiceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000B3039 File Offset: 0x000B1239
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}
	}
}
