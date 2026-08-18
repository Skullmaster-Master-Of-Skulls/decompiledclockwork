using System;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x0200012F RID: 303
	public sealed class SelectorsGroupNode : AstNode
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0004CF1A File Offset: 0x0004B11A
		public SelectorsGroupNode(ReadOnlyCollection<SelectorNode> selectorNodes)
		{
			this.SelectorNodes = selectorNodes;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004CF29 File Offset: 0x0004B129
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0004CF31 File Offset: 0x0004B131
		public ReadOnlyCollection<SelectorNode> SelectorNodes { get; private set; }

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004CF3A File Offset: 0x0004B13A
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitSelectorsGroupNode(this);
		}
	}
}
