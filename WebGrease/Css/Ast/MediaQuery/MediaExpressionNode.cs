using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.MediaQuery
{
	// Token: 0x0200011E RID: 286
	public sealed class MediaExpressionNode : AstNode
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x0004C543 File Offset: 0x0004A743
		public MediaExpressionNode(string mediaFeature, ExprNode exprNode)
		{
			this.MediaFeature = mediaFeature;
			this.ExprNode = exprNode;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004C559 File Offset: 0x0004A759
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x0004C561 File Offset: 0x0004A761
		public string MediaFeature { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004C56A File Offset: 0x0004A76A
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x0004C572 File Offset: 0x0004A772
		public ExprNode ExprNode { get; private set; }

		// Token: 0x0600116F RID: 4463 RVA: 0x0004C57B File Offset: 0x0004A77B
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitMediaExpressionNode(this);
		}
	}
}
