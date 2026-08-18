using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000132 RID: 306
	public sealed class UniversalSelectorNode : AstNode
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x0004D00E File Offset: 0x0004B20E
		public UniversalSelectorNode(SelectorNamespacePrefixNode selectorNamespacePrefixNode)
		{
			this.SelectorNamespacePrefixNode = selectorNamespacePrefixNode;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004D01D File Offset: 0x0004B21D
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0004D025 File Offset: 0x0004B225
		public SelectorNamespacePrefixNode SelectorNamespacePrefixNode { get; private set; }

		// Token: 0x060011FC RID: 4604 RVA: 0x0004D02E File Offset: 0x0004B22E
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitUniversalSelectorNode(this);
		}
	}
}
