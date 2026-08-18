using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x020001F1 RID: 497
	internal abstract class InteriorNode : SyntaxTreeNode
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x000B2C6E File Offset: 0x000B0E6E
		// (set) Token: 0x060020AC RID: 8364 RVA: 0x000B2C76 File Offset: 0x000B0E76
		public SyntaxTreeNode LeftChild
		{
			get
			{
				return this.leftChild;
			}
			set
			{
				this.leftChild = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x000B2C7F File Offset: 0x000B0E7F
		// (set) Token: 0x060020AE RID: 8366 RVA: 0x000B2C87 File Offset: 0x000B0E87
		public SyntaxTreeNode RightChild
		{
			get
			{
				return this.rightChild;
			}
			set
			{
				this.rightChild = value;
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000B2C90 File Offset: 0x000B0E90
		public override SyntaxTreeNode Clone(Positions positions)
		{
			InteriorNode interiorNode = (InteriorNode)base.MemberwiseClone();
			interiorNode.LeftChild = this.leftChild.Clone(positions);
			if (this.rightChild != null)
			{
				interiorNode.RightChild = this.rightChild.Clone(positions);
			}
			return interiorNode;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000B2CD8 File Offset: 0x000B0ED8
		protected void ExpandTreeNoRecursive(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			Stack<InteriorNode> stack = new Stack<InteriorNode>();
			InteriorNode interiorNode = this;
			while (interiorNode.leftChild is ChoiceNode || interiorNode.leftChild is SequenceNode)
			{
				stack.Push(interiorNode);
				interiorNode = (InteriorNode)interiorNode.leftChild;
			}
			interiorNode.leftChild.ExpandTree(interiorNode, symbols, positions);
			for (;;)
			{
				if (interiorNode.rightChild != null)
				{
					interiorNode.rightChild.ExpandTree(interiorNode, symbols, positions);
				}
				if (stack.Count == 0)
				{
					break;
				}
				interiorNode = stack.Pop();
			}
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000B2D51 File Offset: 0x000B0F51
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			this.leftChild.ExpandTree(this, symbols, positions);
			if (this.rightChild != null)
			{
				this.rightChild.ExpandTree(this, symbols, positions);
			}
		}

		// Token: 0x04000DBD RID: 3517
		private SyntaxTreeNode leftChild;

		// Token: 0x04000DBE RID: 3518
		private SyntaxTreeNode rightChild;
	}
}
