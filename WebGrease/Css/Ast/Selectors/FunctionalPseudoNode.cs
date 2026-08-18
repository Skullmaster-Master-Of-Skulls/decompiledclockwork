using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000127 RID: 295
	public sealed class FunctionalPseudoNode : AstNode
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x0004CAA9 File Offset: 0x0004ACA9
		public FunctionalPseudoNode(string functionName, SelectorExpressionNode selectorExpressionNode)
		{
			this.FunctionName = functionName;
			this.SelectorExpressionNode = selectorExpressionNode;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0004CABF File Offset: 0x0004ACBF
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x0004CAC7 File Offset: 0x0004ACC7
		public string FunctionName { get; private set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004CAD0 File Offset: 0x0004ACD0
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x0004CAD8 File Offset: 0x0004ACD8
		public SelectorExpressionNode SelectorExpressionNode { get; private set; }

		// Token: 0x060011AC RID: 4524 RVA: 0x0004CAE1 File Offset: 0x0004ACE1
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitFunctionalPseudoNode(this);
		}
	}
}
