using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x0200012E RID: 302
	public sealed class SelectorNode : AstNode
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x0004CECA File Offset: 0x0004B0CA
		public SelectorNode(SimpleSelectorSequenceNode simpleSelectorSequenceNode, ReadOnlyCollection<CombinatorSimpleSelectorSequenceNode> combinatorSimpleSelectorSequenceNodes)
		{
			this.SimpleSelectorSequenceNode = simpleSelectorSequenceNode;
			this.CombinatorSimpleSelectorSequenceNodes = (combinatorSimpleSelectorSequenceNodes ?? new List<CombinatorSimpleSelectorSequenceNode>(0).AsReadOnly());
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004CEEF File Offset: 0x0004B0EF
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004CEF7 File Offset: 0x0004B0F7
		public SimpleSelectorSequenceNode SimpleSelectorSequenceNode { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004CF00 File Offset: 0x0004B100
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x0004CF08 File Offset: 0x0004B108
		public ReadOnlyCollection<CombinatorSimpleSelectorSequenceNode> CombinatorSimpleSelectorSequenceNodes { get; private set; }

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004CF11 File Offset: 0x0004B111
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitSelectorNode(this);
		}
	}
}
