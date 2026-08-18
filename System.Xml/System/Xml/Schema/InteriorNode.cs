using System;

namespace System.Xml.Schema
{
	// Token: 0x02000199 RID: 409
	internal abstract class InteriorNode : SyntaxTreeNode
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0005EFE6 File Offset: 0x0005DFE6
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0005EFEE File Offset: 0x0005DFEE
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

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0005EFF7 File Offset: 0x0005DFF7
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0005EFFF File Offset: 0x0005DFFF
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

		// Token: 0x06001564 RID: 5476 RVA: 0x0005F008 File Offset: 0x0005E008
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

		// Token: 0x06001565 RID: 5477 RVA: 0x0005F04E File Offset: 0x0005E04E
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			this.leftChild.ExpandTree(this, symbols, positions);
			if (this.rightChild != null)
			{
				this.rightChild.ExpandTree(this, symbols, positions);
			}
		}

		// Token: 0x04000CC8 RID: 3272
		private SyntaxTreeNode leftChild;

		// Token: 0x04000CC9 RID: 3273
		private SyntaxTreeNode rightChild;
	}
}
