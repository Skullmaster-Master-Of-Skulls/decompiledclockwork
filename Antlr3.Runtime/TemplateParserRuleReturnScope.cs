using System;

namespace Antlr.Runtime
{
	// Token: 0x02000034 RID: 52
	public class TemplateParserRuleReturnScope<TTemplate, TToken> : ParserRuleReturnScope<TToken>, ITemplateRuleReturnScope<TTemplate>, ITemplateRuleReturnScope
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00006E75 File Offset: 0x00005075
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00006E7D File Offset: 0x0000507D
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00006E86 File Offset: 0x00005086
		object ITemplateRuleReturnScope.Template
		{
			get
			{
				return this.Template;
			}
		}

		// Token: 0x04000079 RID: 121
		private TTemplate _template;
	}
}
