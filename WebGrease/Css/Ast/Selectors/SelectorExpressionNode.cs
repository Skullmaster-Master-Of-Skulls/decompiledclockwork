using System;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x0200012C RID: 300
	public sealed class SelectorExpressionNode : AstNode
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0004CE69 File Offset: 0x0004B069
		public SelectorExpressionNode(ReadOnlyCollection<string> selectorExpressions)
		{
			this.SelectorExpressions = selectorExpressions;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004CE78 File Offset: 0x0004B078
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0004CE80 File Offset: 0x0004B080
		public ReadOnlyCollection<string> SelectorExpressions { get; private set; }

		// Token: 0x060011DA RID: 4570 RVA: 0x0004CE89 File Offset: 0x0004B089
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitSelectorExpressionNode(this);
		}
	}
}
