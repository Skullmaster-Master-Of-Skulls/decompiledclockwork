using System;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000122 RID: 290
	public sealed class PageNode : StyleSheetRuleNode
	{
		// Token: 0x06001188 RID: 4488 RVA: 0x0004C6BD File Offset: 0x0004A8BD
		public PageNode(string pseudoPage, ReadOnlyCollection<DeclarationNode> declarations)
		{
			this.PseudoPage = pseudoPage;
			this.Declarations = declarations;
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004C6D3 File Offset: 0x0004A8D3
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x0004C6DB File Offset: 0x0004A8DB
		public string PseudoPage { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004C6E4 File Offset: 0x0004A8E4
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x0004C6EC File Offset: 0x0004A8EC
		public ReadOnlyCollection<DeclarationNode> Declarations { get; private set; }

		// Token: 0x0600118D RID: 4493 RVA: 0x0004C6F5 File Offset: 0x0004A8F5
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitPageNode(this);
		}
	}
}
