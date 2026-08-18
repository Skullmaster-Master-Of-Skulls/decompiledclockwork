using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000124 RID: 292
	public sealed class AttribNode : AstNode
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x0004C9E9 File Offset: 0x0004ABE9
		public AttribNode(SelectorNamespacePrefixNode selectorNamespacePrefixNode, string identity, AttribOperatorAndValueNode attribOperatorAndValueNode)
		{
			this.SelectorNamespacePrefixNode = selectorNamespacePrefixNode;
			this.Ident = identity;
			this.OperatorAndValueNode = (attribOperatorAndValueNode ?? new AttribOperatorAndValueNode(AttribOperatorKind.None, string.Empty));
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004CA15 File Offset: 0x0004AC15
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x0004CA1D File Offset: 0x0004AC1D
		public SelectorNamespacePrefixNode SelectorNamespacePrefixNode { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004CA26 File Offset: 0x0004AC26
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x0004CA2E File Offset: 0x0004AC2E
		public string Ident { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0004CA37 File Offset: 0x0004AC37
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x0004CA3F File Offset: 0x0004AC3F
		public AttribOperatorAndValueNode OperatorAndValueNode { get; private set; }

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004CA48 File Offset: 0x0004AC48
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitAttribNode(this);
		}
	}
}
