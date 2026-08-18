using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000125 RID: 293
	public sealed class AttribOperatorAndValueNode : AstNode
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x0004CA51 File Offset: 0x0004AC51
		public AttribOperatorAndValueNode(AttribOperatorKind operatorKind, string identityOrString)
		{
			if (string.IsNullOrWhiteSpace(identityOrString) && operatorKind != AttribOperatorKind.None)
			{
				throw new AstException(CssStrings.ExpectedIdentifierOrString);
			}
			this.AttribOperatorKind = operatorKind;
			this.IdentOrString = identityOrString;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0004CA7E File Offset: 0x0004AC7E
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x0004CA86 File Offset: 0x0004AC86
		public AttribOperatorKind AttribOperatorKind { get; private set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004CA8F File Offset: 0x0004AC8F
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0004CA97 File Offset: 0x0004AC97
		public string IdentOrString { get; private set; }

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004CAA0 File Offset: 0x0004ACA0
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitAttribOperatorAndValueNode(this);
		}
	}
}
