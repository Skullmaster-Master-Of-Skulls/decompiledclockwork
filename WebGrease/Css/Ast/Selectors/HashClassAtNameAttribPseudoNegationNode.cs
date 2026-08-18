using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Selectors
{
	// Token: 0x02000128 RID: 296
	public sealed class HashClassAtNameAttribPseudoNegationNode : AstNode
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x0004CAEC File Offset: 0x0004ACEC
		public HashClassAtNameAttribPseudoNegationNode(string hash, string cssClass, string replacementToken, string atName, AttribNode attribNode, PseudoNode pseudoNode, NegationNode negationNode)
		{
			if (!string.IsNullOrWhiteSpace(hash))
			{
				if (!string.IsNullOrWhiteSpace(cssClass) || !string.IsNullOrWhiteSpace(atName) || !string.IsNullOrWhiteSpace(replacementToken) || attribNode != null || pseudoNode != null || negationNode != null)
				{
					throw new AstException("Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.");
				}
			}
			else if (!string.IsNullOrWhiteSpace(cssClass))
			{
				if (!string.IsNullOrWhiteSpace(replacementToken) || !string.IsNullOrWhiteSpace(atName) || attribNode != null || pseudoNode != null || negationNode != null)
				{
					throw new AstException("Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.");
				}
			}
			else if (!string.IsNullOrWhiteSpace(replacementToken))
			{
				if (!string.IsNullOrWhiteSpace(atName) || attribNode != null || pseudoNode != null || negationNode != null)
				{
					throw new AstException("Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.");
				}
			}
			else if (!string.IsNullOrWhiteSpace(atName))
			{
				if (attribNode != null || pseudoNode != null || negationNode != null)
				{
					throw new AstException("Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.");
				}
			}
			else if (attribNode != null)
			{
				if (pseudoNode != null || negationNode != null)
				{
					throw new AstException("Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.");
				}
			}
			else if (pseudoNode != null && negationNode != null)
			{
				throw new AstException("Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.");
			}
			this.Hash = hash;
			this.ReplacementToken = replacementToken;
			this.CssClass = cssClass;
			this.AtName = atName;
			this.AttribNode = attribNode;
			this.PseudoNode = pseudoNode;
			this.NegationNode = negationNode;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0004CC11 File Offset: 0x0004AE11
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0004CC19 File Offset: 0x0004AE19
		public string Hash { get; private set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0004CC22 File Offset: 0x0004AE22
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0004CC2A File Offset: 0x0004AE2A
		public string ReplacementToken { get; private set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004CC33 File Offset: 0x0004AE33
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0004CC3B File Offset: 0x0004AE3B
		public string CssClass { get; private set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004CC44 File Offset: 0x0004AE44
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0004CC4C File Offset: 0x0004AE4C
		public string AtName { get; private set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004CC55 File Offset: 0x0004AE55
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0004CC5D File Offset: 0x0004AE5D
		public AttribNode AttribNode { get; private set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004CC66 File Offset: 0x0004AE66
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0004CC6E File Offset: 0x0004AE6E
		public PseudoNode PseudoNode { get; private set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004CC77 File Offset: 0x0004AE77
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x0004CC7F File Offset: 0x0004AE7F
		public NegationNode NegationNode { get; private set; }

		// Token: 0x060011BC RID: 4540 RVA: 0x0004CC88 File Offset: 0x0004AE88
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitHashClassAtNameAttribPseudoNegationNode(this);
		}

		// Token: 0x0400071C RID: 1820
		private const string ExceptionMessage = "Only a single value out of hash or class or at name or attrib node or pseudo node or negation node can be not null.";
	}
}
