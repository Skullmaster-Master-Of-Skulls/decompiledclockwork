using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x0200012D RID: 301
	public sealed class SelectorNamespacePrefixNode : AstNode
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x0004CE92 File Offset: 0x0004B092
		public SelectorNamespacePrefixNode(string prefix)
		{
			if (string.IsNullOrWhiteSpace(prefix))
			{
				prefix = string.Empty;
			}
			this.Prefix = prefix;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004CEB8 File Offset: 0x0004B0B8
		public string Prefix { get; private set; }

		// Token: 0x060011DE RID: 4574 RVA: 0x0004CEC1 File Offset: 0x0004B0C1
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitSelectorNamespacePrefixNode(this);
		}
	}
}
