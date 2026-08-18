using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000056 RID: 86
	public class TemplateTreeRuleReturnScope<TTemplate, TTree> : TreeRuleReturnScope<TTree>, ITemplateRuleReturnScope<TTemplate>, ITemplateRuleReturnScope
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000A7A5 File Offset: 0x000089A5
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000A7AD File Offset: 0x000089AD
		public TTemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A7B6 File Offset: 0x000089B6
		object ITemplateRuleReturnScope.Template
		{
			get
			{
				return this.Template;
			}
		}

		// Token: 0x040000C8 RID: 200
		private TTemplate _template;
	}
}
