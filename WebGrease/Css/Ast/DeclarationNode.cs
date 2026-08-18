using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000119 RID: 281
	public sealed class DeclarationNode : AstNode
	{
		// Token: 0x0600113A RID: 4410 RVA: 0x0004C165 File Offset: 0x0004A365
		public DeclarationNode(string property, ExprNode exprNode, string prio, ReadOnlyCollection<ImportantCommentNode> importantComments)
		{
			this.Property = property;
			this.ExprNode = exprNode;
			this.Prio = (prio ?? string.Empty);
			this.ImportantComments = (importantComments ?? new List<ImportantCommentNode>().AsReadOnly());
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004C1A1 File Offset: 0x0004A3A1
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0004C1A9 File Offset: 0x0004A3A9
		public ReadOnlyCollection<ImportantCommentNode> ImportantComments { get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004C1B2 File Offset: 0x0004A3B2
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0004C1BA File Offset: 0x0004A3BA
		public string Property { get; private set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004C1C3 File Offset: 0x0004A3C3
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0004C1CB File Offset: 0x0004A3CB
		public ExprNode ExprNode { get; private set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0004C1D4 File Offset: 0x0004A3D4
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0004C1DC File Offset: 0x0004A3DC
		public string Prio { get; private set; }

		// Token: 0x06001143 RID: 4419 RVA: 0x0004C1E5 File Offset: 0x0004A3E5
		public bool Equals(DeclarationNode declarationNode)
		{
			return declarationNode.Property.Equals(this.Property) && declarationNode.ExprNode.Equals(this.ExprNode) && declarationNode.Prio.Equals(this.Prio);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0004C220 File Offset: 0x0004A420
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitDeclarationNode(this);
		}
	}
}
