using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000130 RID: 304
	public sealed class SimpleSelectorSequenceNode : AstNode
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0004CF43 File Offset: 0x0004B143
		public SimpleSelectorSequenceNode(TypeSelectorNode typeSelectorNode, UniversalSelectorNode universalSelectorNode, string separator, ReadOnlyCollection<HashClassAtNameAttribPseudoNegationNode> simpleSelectorValues)
		{
			this.TypeSelectorNode = typeSelectorNode;
			this.UniversalSelectorNode = universalSelectorNode;
			this.Separator = (separator ?? string.Empty);
			this.HashClassAttribPseudoNegationNodes = (simpleSelectorValues ?? new List<HashClassAtNameAttribPseudoNegationNode>(0).AsReadOnly());
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004CF80 File Offset: 0x0004B180
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0004CF88 File Offset: 0x0004B188
		public TypeSelectorNode TypeSelectorNode { get; private set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004CF91 File Offset: 0x0004B191
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0004CF99 File Offset: 0x0004B199
		public UniversalSelectorNode UniversalSelectorNode { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004CFA2 File Offset: 0x0004B1A2
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0004CFAA File Offset: 0x0004B1AA
		public string Separator { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004CFB3 File Offset: 0x0004B1B3
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004CFBB File Offset: 0x0004B1BB
		public ReadOnlyCollection<HashClassAtNameAttribPseudoNegationNode> HashClassAttribPseudoNegationNodes { get; private set; }

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004CFC4 File Offset: 0x0004B1C4
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitSimpleSelectorSequenceNode(this);
		}
	}
}
