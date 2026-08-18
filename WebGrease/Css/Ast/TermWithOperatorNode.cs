using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000135 RID: 309
	public sealed class TermWithOperatorNode : AstNode
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
		public TermWithOperatorNode(string op, TermNode termNode)
		{
			if (string.IsNullOrWhiteSpace(op))
			{
				op = ' '.ToString();
			}
			this.Operator = op;
			this.TermNode = termNode;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004D2FD File Offset: 0x0004B4FD
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0004D305 File Offset: 0x0004B505
		public bool UsesBinary
		{
			get
			{
				return this.usesBinary;
			}
			set
			{
				this.usesBinary = value;
				this.TermNode.IsBinary = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004D31A File Offset: 0x0004B51A
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0004D322 File Offset: 0x0004B522
		public string Operator { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004D32B File Offset: 0x0004B52B
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0004D333 File Offset: 0x0004B533
		public TermNode TermNode { get; private set; }

		// Token: 0x06001221 RID: 4641 RVA: 0x0004D33C File Offset: 0x0004B53C
		public bool Equals(TermWithOperatorNode termWithOperator)
		{
			return termWithOperator.UsesBinary == this.UsesBinary && termWithOperator.TermNode.Equals(this.TermNode) && termWithOperator.Operator.Equals(this.Operator);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0004D372 File Offset: 0x0004B572
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitTermWithOperatorNode(this);
		}

		// Token: 0x04000747 RID: 1863
		private bool usesBinary;
	}
}
