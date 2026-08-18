using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x0200011B RID: 283
	public sealed class ExprNode : AstNode
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x0004C282 File Offset: 0x0004A482
		public ExprNode(TermNode termNode, ReadOnlyCollection<TermWithOperatorNode> termsWithOperators, ReadOnlyCollection<ImportantCommentNode> importantComments)
		{
			this.TermNode = termNode;
			this.TermsWithOperators = (termsWithOperators ?? new List<TermWithOperatorNode>().AsReadOnly());
			this.ImportantComments = (importantComments ?? new List<ImportantCommentNode>().AsReadOnly());
			this.UsesBinary = false;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004C2C2 File Offset: 0x0004A4C2
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x0004C2CC File Offset: 0x0004A4CC
		public bool UsesBinary
		{
			get
			{
				return this.usesBinary;
			}
			set
			{
				this.usesBinary = value;
				foreach (TermWithOperatorNode termWithOperatorNode in this.TermsWithOperators)
				{
					termWithOperatorNode.UsesBinary = value;
				}
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004C320 File Offset: 0x0004A520
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x0004C328 File Offset: 0x0004A528
		public ReadOnlyCollection<ImportantCommentNode> ImportantComments { get; private set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0004C331 File Offset: 0x0004A531
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x0004C339 File Offset: 0x0004A539
		public TermNode TermNode { get; private set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0004C342 File Offset: 0x0004A542
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0004C34A File Offset: 0x0004A54A
		public ReadOnlyCollection<TermWithOperatorNode> TermsWithOperators { get; private set; }

		// Token: 0x06001156 RID: 4438 RVA: 0x0004C354 File Offset: 0x0004A554
		public bool Equals(ExprNode exprNode)
		{
			if (!exprNode.TermNode.Equals(this.TermNode) || exprNode.UsesBinary != this.UsesBinary || exprNode.TermsWithOperators.Count != this.TermsWithOperators.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TermsWithOperators.Count; i++)
			{
				if (!exprNode.TermsWithOperators[i].Equals(this.TermsWithOperators[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0004C3D4 File Offset: 0x0004A5D4
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitExprNode(this);
		}

		// Token: 0x040006F2 RID: 1778
		private bool usesBinary;
	}
}
