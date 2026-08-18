using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x0200012A RID: 298
	public sealed class NegationNode : AstNode
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x0004CDE7 File Offset: 0x0004AFE7
		public NegationNode(NegationArgNode negationArgNode)
		{
			this.NegationArgNode = negationArgNode;
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0004CDF6 File Offset: 0x0004AFF6
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x0004CDFE File Offset: 0x0004AFFE
		public NegationArgNode NegationArgNode { get; private set; }

		// Token: 0x060011CE RID: 4558 RVA: 0x0004CE07 File Offset: 0x0004B007
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitNegationNode(this);
		}
	}
}
