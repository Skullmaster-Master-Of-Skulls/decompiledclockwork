using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x0200012B RID: 299
	public sealed class PseudoNode : AstNode
	{
		// Token: 0x060011CF RID: 4559 RVA: 0x0004CE10 File Offset: 0x0004B010
		public PseudoNode(int numberOfColons, string ident, FunctionalPseudoNode functionalPseudoNode)
		{
			this.NumberOfColons = numberOfColons;
			this.Ident = ident;
			this.FunctionalPseudoNode = functionalPseudoNode;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004CE2D File Offset: 0x0004B02D
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x0004CE35 File Offset: 0x0004B035
		public int NumberOfColons { get; private set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004CE3E File Offset: 0x0004B03E
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x0004CE46 File Offset: 0x0004B046
		public string Ident { get; private set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004CE4F File Offset: 0x0004B04F
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x0004CE57 File Offset: 0x0004B057
		public FunctionalPseudoNode FunctionalPseudoNode { get; private set; }

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004CE60 File Offset: 0x0004B060
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitPseudoNode(this);
		}
	}
}
