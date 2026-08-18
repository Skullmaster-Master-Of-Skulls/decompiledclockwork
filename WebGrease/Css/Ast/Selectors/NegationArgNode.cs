using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000129 RID: 297
	public sealed class NegationArgNode : AstNode
	{
		// Token: 0x060011BD RID: 4541 RVA: 0x0004CC94 File Offset: 0x0004AE94
		public NegationArgNode(TypeSelectorNode typeSelectorNode, UniversalSelectorNode universalSelectorNode, string hash, string cssClass, AttribNode attribNode, PseudoNode pseudoNode)
		{
			if (typeSelectorNode != null && (universalSelectorNode != null || !string.IsNullOrWhiteSpace(hash) || !string.IsNullOrWhiteSpace(cssClass) || attribNode != null || pseudoNode != null))
			{
				throw new AstException("Only a single value out of type selector, universal selector, hash or class or attrib node or pseudo node can be not null.");
			}
			if (universalSelectorNode != null && (!string.IsNullOrWhiteSpace(hash) || !string.IsNullOrWhiteSpace(cssClass) || attribNode != null || pseudoNode != null))
			{
				throw new AstException("Only a single value out of type selector, universal selector, hash or class or attrib node or pseudo node can be not null.");
			}
			if (!string.IsNullOrWhiteSpace(hash) && (!string.IsNullOrWhiteSpace(cssClass) || attribNode != null || pseudoNode != null))
			{
				throw new AstException("Only a single value out of type selector, universal selector, hash or class or attrib node or pseudo node can be not null.");
			}
			if (!string.IsNullOrWhiteSpace(cssClass) && (attribNode != null || pseudoNode != null))
			{
				throw new AstException("Only a single value out of type selector, universal selector, hash or class or attrib node or pseudo node can be not null.");
			}
			if (attribNode != null && pseudoNode != null)
			{
				throw new AstException("Only a single value out of type selector, universal selector, hash or class or attrib node or pseudo node can be not null.");
			}
			this.TypeSelectorNode = typeSelectorNode;
			this.UniversalSelectorNode = universalSelectorNode;
			this.Hash = hash;
			this.CssClass = cssClass;
			this.AttribNode = attribNode;
			this.PseudoNode = pseudoNode;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004CD78 File Offset: 0x0004AF78
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x0004CD80 File Offset: 0x0004AF80
		public TypeSelectorNode TypeSelectorNode { get; private set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0004CD89 File Offset: 0x0004AF89
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x0004CD91 File Offset: 0x0004AF91
		public UniversalSelectorNode UniversalSelectorNode { get; private set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004CD9A File Offset: 0x0004AF9A
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x0004CDA2 File Offset: 0x0004AFA2
		public string Hash { get; private set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0004CDAB File Offset: 0x0004AFAB
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x0004CDB3 File Offset: 0x0004AFB3
		public string CssClass { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004CDBC File Offset: 0x0004AFBC
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x0004CDC4 File Offset: 0x0004AFC4
		public AttribNode AttribNode { get; private set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004CDCD File Offset: 0x0004AFCD
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x0004CDD5 File Offset: 0x0004AFD5
		public PseudoNode PseudoNode { get; private set; }

		// Token: 0x060011CA RID: 4554 RVA: 0x0004CDDE File Offset: 0x0004AFDE
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitNegationArgNode(this);
		}

		// Token: 0x04000724 RID: 1828
		private const string ExceptionMessage = "Only a single value out of type selector, universal selector, hash or class or attrib node or pseudo node can be not null.";
	}
}
