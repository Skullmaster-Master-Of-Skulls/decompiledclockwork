using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.MediaQuery
{
	// Token: 0x0200011F RID: 287
	public sealed class MediaNode : StyleSheetRuleNode
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x0004C584 File Offset: 0x0004A784
		public MediaNode(ReadOnlyCollection<MediaQueryNode> mediaQueries, ReadOnlyCollection<RulesetNode> rulesets, ReadOnlyCollection<PageNode> pages)
		{
			this.MediaQueries = mediaQueries;
			this.Rulesets = (rulesets ?? new List<RulesetNode>(0).AsReadOnly());
			this.PageNodes = (pages ?? new List<PageNode>(0).AsReadOnly());
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0004C5BF File Offset: 0x0004A7BF
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x0004C5C7 File Offset: 0x0004A7C7
		public ReadOnlyCollection<MediaQueryNode> MediaQueries { get; private set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004C5D0 File Offset: 0x0004A7D0
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x0004C5D8 File Offset: 0x0004A7D8
		public ReadOnlyCollection<RulesetNode> Rulesets { get; private set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004C5E1 File Offset: 0x0004A7E1
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x0004C5E9 File Offset: 0x0004A7E9
		public ReadOnlyCollection<PageNode> PageNodes { get; private set; }

		// Token: 0x06001177 RID: 4471 RVA: 0x0004C5F2 File Offset: 0x0004A7F2
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitMediaNode(this);
		}
	}
}
