using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000133 RID: 307
	public sealed class StyleSheetNode : AstNode
	{
		// Token: 0x060011FD RID: 4605 RVA: 0x0004D038 File Offset: 0x0004B238
		public StyleSheetNode(string charSet, ReadOnlyCollection<ImportNode> imports, ReadOnlyCollection<NamespaceNode> namespaces, ReadOnlyCollection<StyleSheetRuleNode> styleSheetRules)
		{
			this.CharSetString = (charSet ?? string.Empty);
			this.Imports = (imports ?? new List<ImportNode>(0).AsReadOnly());
			this.Namespaces = (namespaces ?? new List<NamespaceNode>(0).AsReadOnly());
			this.StyleSheetRules = (styleSheetRules ?? new List<StyleSheetRuleNode>(0).AsReadOnly());
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004D09E File Offset: 0x0004B29E
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0004D0A6 File Offset: 0x0004B2A6
		public string CharSetString { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004D0AF File Offset: 0x0004B2AF
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x0004D0B7 File Offset: 0x0004B2B7
		public ReadOnlyCollection<ImportNode> Imports { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004D0C0 File Offset: 0x0004B2C0
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		public ReadOnlyCollection<NamespaceNode> Namespaces { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0004D0D1 File Offset: 0x0004B2D1
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0004D0D9 File Offset: 0x0004B2D9
		public ReadOnlyCollection<StyleSheetRuleNode> StyleSheetRules { get; private set; }

		// Token: 0x06001206 RID: 4614 RVA: 0x0004D0E2 File Offset: 0x0004B2E2
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitStyleSheetNode(this);
		}
	}
}
