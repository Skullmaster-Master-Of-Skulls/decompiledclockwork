using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x020001F2 RID: 498
	internal sealed class SequenceNode : InteriorNode
	{
		// Token: 0x060020B3 RID: 8371 RVA: 0x000B2D80 File Offset: 0x000B0F80
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			Stack<SequenceNode.SequenceConstructPosContext> stack = new Stack<SequenceNode.SequenceConstructPosContext>();
			SequenceNode.SequenceConstructPosContext sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext(this, firstpos, lastpos);
			SequenceNode this_;
			for (;;)
			{
				this_ = sequenceConstructPosContext.this_;
				sequenceConstructPosContext.lastposLeft = new BitSet(lastpos.Count);
				if (!(this_.LeftChild is SequenceNode))
				{
					break;
				}
				stack.Push(sequenceConstructPosContext);
				sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext((SequenceNode)this_.LeftChild, sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft);
			}
			this_.LeftChild.ConstructPos(sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft, followpos);
			for (;;)
			{
				sequenceConstructPosContext.firstposRight = new BitSet(firstpos.Count);
				this_.RightChild.ConstructPos(sequenceConstructPosContext.firstposRight, sequenceConstructPosContext.lastpos, followpos);
				if (this_.LeftChild.IsNullable && !this_.RightChild.IsRangeNode)
				{
					sequenceConstructPosContext.firstpos.Or(sequenceConstructPosContext.firstposRight);
				}
				if (this_.RightChild.IsNullable)
				{
					sequenceConstructPosContext.lastpos.Or(sequenceConstructPosContext.lastposLeft);
				}
				for (int num = sequenceConstructPosContext.lastposLeft.NextSet(-1); num != -1; num = sequenceConstructPosContext.lastposLeft.NextSet(num))
				{
					followpos[num].Or(sequenceConstructPosContext.firstposRight);
				}
				if (this_.RightChild.IsRangeNode)
				{
					((LeafRangeNode)this_.RightChild).NextIteration = sequenceConstructPosContext.firstpos.Clone();
				}
				if (stack.Count == 0)
				{
					break;
				}
				sequenceConstructPosContext = stack.Pop();
				this_ = sequenceConstructPosContext.this_;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x000B2EE8 File Offset: 0x000B10E8
		public override bool IsNullable
		{
			get
			{
				SequenceNode sequenceNode = this;
				while (!sequenceNode.RightChild.IsRangeNode || !(((LeafRangeNode)sequenceNode.RightChild).Min == 0m))
				{
					if (!sequenceNode.RightChild.IsNullable && !sequenceNode.RightChild.IsRangeNode)
					{
						return false;
					}
					SyntaxTreeNode leftChild = sequenceNode.LeftChild;
					sequenceNode = (leftChild as SequenceNode);
					if (sequenceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000B2F55 File Offset: 0x000B1155
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}

		// Token: 0x0200048D RID: 1165
		private struct SequenceConstructPosContext
		{
			// Token: 0x06003121 RID: 12577 RVA: 0x0011E096 File Offset: 0x0011C296
			public SequenceConstructPosContext(SequenceNode node, BitSet firstpos, BitSet lastpos)
			{
				this.this_ = node;
				this.firstpos = firstpos;
				this.lastpos = lastpos;
				this.lastposLeft = null;
				this.firstposRight = null;
			}

			// Token: 0x04001E13 RID: 7699
			public SequenceNode this_;

			// Token: 0x04001E14 RID: 7700
			public BitSet firstpos;

			// Token: 0x04001E15 RID: 7701
			public BitSet lastpos;

			// Token: 0x04001E16 RID: 7702
			public BitSet lastposLeft;

			// Token: 0x04001E17 RID: 7703
			public BitSet firstposRight;
		}
	}
}
