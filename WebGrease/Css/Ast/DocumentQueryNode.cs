using System;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x0200011A RID: 282
	public sealed class DocumentQueryNode : StyleSheetRuleNode
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0004C229 File Offset: 0x0004A429
		public DocumentQueryNode(string matchFunctionName, string documentSymbol, ReadOnlyCollection<RulesetNode> rulesets)
		{
			this.Rulesets = rulesets;
			this.MatchFunctionName = matchFunctionName;
			this.DocumentSymbol = documentSymbol;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0004C246 File Offset: 0x0004A446
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x0004C24E File Offset: 0x0004A44E
		public string MatchFunctionName { get; private set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004C257 File Offset: 0x0004A457
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0004C25F File Offset: 0x0004A45F
		public string DocumentSymbol { get; private set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0004C268 File Offset: 0x0004A468
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0004C270 File Offset: 0x0004A470
		public ReadOnlyCollection<RulesetNode> Rulesets { get; private set; }

		// Token: 0x0600114C RID: 4428 RVA: 0x0004C279 File Offset: 0x0004A479
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitDocumentQueryNode(this);
		}
	}
}
