using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000131 RID: 305
	public sealed class TypeSelectorNode : AstNode
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x0004CFCD File Offset: 0x0004B1CD
		public TypeSelectorNode(SelectorNamespacePrefixNode selectorNamespacePrefixNode, string elementName)
		{
			this.SelectorNamespacePrefixNode = selectorNamespacePrefixNode;
			this.ElementName = elementName;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004CFE3 File Offset: 0x0004B1E3
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0004CFEB File Offset: 0x0004B1EB
		public SelectorNamespacePrefixNode SelectorNamespacePrefixNode { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004CFF4 File Offset: 0x0004B1F4
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public string ElementName { get; private set; }

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004D005 File Offset: 0x0004B205
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitTypeSelectorNode(this);
		}
	}
}
