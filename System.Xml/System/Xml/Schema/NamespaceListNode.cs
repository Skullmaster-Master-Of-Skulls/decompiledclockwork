using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000198 RID: 408
	internal class NamespaceListNode : SyntaxTreeNode
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x0005EEF1 File Offset: 0x0005DEF1
		public NamespaceListNode(NamespaceList namespaceList, object particle)
		{
			this.namespaceList = namespaceList;
			this.particle = particle;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0005EF07 File Offset: 0x0005DF07
		public override SyntaxTreeNode Clone(Positions positions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0005EF0E File Offset: 0x0005DF0E
		public virtual ICollection GetResolvedSymbols(SymbolsDictionary symbols)
		{
			return symbols.GetNamespaceListSymbols(this.namespaceList);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005EF1C File Offset: 0x0005DF1C
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			SyntaxTreeNode syntaxTreeNode = null;
			foreach (object obj in this.GetResolvedSymbols(symbols))
			{
				int symbol = (int)obj;
				if (symbols.GetParticle(symbol) != this.particle)
				{
					symbols.IsUpaEnforced = false;
				}
				LeafNode leafNode = new LeafNode(positions.Add(symbol, this.particle));
				if (syntaxTreeNode == null)
				{
					syntaxTreeNode = leafNode;
				}
				else
				{
					syntaxTreeNode = new ChoiceNode
					{
						LeftChild = syntaxTreeNode,
						RightChild = leafNode
					};
				}
			}
			if (parent.LeftChild == this)
			{
				parent.LeftChild = syntaxTreeNode;
				return;
			}
			parent.RightChild = syntaxTreeNode;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0005EFD8 File Offset: 0x0005DFD8
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0005EFDF File Offset: 0x0005DFDF
		public override bool IsNullable
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04000CC6 RID: 3270
		protected NamespaceList namespaceList;

		// Token: 0x04000CC7 RID: 3271
		protected object particle;
	}
}
