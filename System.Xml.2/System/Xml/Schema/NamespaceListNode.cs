using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001F0 RID: 496
	internal class NamespaceListNode : SyntaxTreeNode
	{
		// Token: 0x060020A5 RID: 8357 RVA: 0x000B2B79 File Offset: 0x000B0D79
		public NamespaceListNode(NamespaceList namespaceList, object particle)
		{
			this.namespaceList = namespaceList;
			this.particle = particle;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000B2B8F File Offset: 0x000B0D8F
		public override SyntaxTreeNode Clone(Positions positions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000B2B96 File Offset: 0x000B0D96
		public virtual ICollection GetResolvedSymbols(SymbolsDictionary symbols)
		{
			return symbols.GetNamespaceListSymbols(this.namespaceList);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000B2BA4 File Offset: 0x000B0DA4
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

		// Token: 0x060020A9 RID: 8361 RVA: 0x000B2C60 File Offset: 0x000B0E60
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x000B2C67 File Offset: 0x000B0E67
		public override bool IsNullable
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04000DBB RID: 3515
		protected NamespaceList namespaceList;

		// Token: 0x04000DBC RID: 3516
		protected object particle;
	}
}
