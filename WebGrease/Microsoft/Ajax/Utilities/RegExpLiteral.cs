using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000BE RID: 190
	public sealed class RegExpLiteral : Expression
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003D59D File Offset: 0x0003B79D
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x0003D5A5 File Offset: 0x0003B7A5
		public string Pattern { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003D5AE File Offset: 0x0003B7AE
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003D5B6 File Offset: 0x0003B7B6
		public string PatternSwitches { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003D5BF File Offset: 0x0003B7BF
		public override bool IsConstant
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003D5C2 File Offset: 0x0003B7C2
		public RegExpLiteral(Context context) : base(context)
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003D5CB File Offset: 0x0003B7CB
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			RegExpLiteral regExpLiteral = otherNode as RegExpLiteral;
			return regExpLiteral != null && string.CompareOrdinal(this.Pattern, regExpLiteral.Pattern) == 0 && string.CompareOrdinal(this.PatternSwitches, regExpLiteral.PatternSwitches) == 0;
		}
	}
}
