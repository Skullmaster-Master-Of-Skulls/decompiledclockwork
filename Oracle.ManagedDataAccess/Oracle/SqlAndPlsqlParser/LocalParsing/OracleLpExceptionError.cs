using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B3 RID: 691
	public enum OracleLpExceptionError
	{
		// Token: 0x04001C4A RID: 7242
		InvalidRuleHeadSymbol,
		// Token: 0x04001C4B RID: 7243
		InvalidRuleRHSSymbols,
		// Token: 0x04001C4C RID: 7244
		MissingParserSymbol,
		// Token: 0x04001C4D RID: 7245
		MissingRule,
		// Token: 0x04001C4E RID: 7246
		MissingTable_View_Query,
		// Token: 0x04001C4F RID: 7247
		MissingColumnFromReference,
		// Token: 0x04001C50 RID: 7248
		MissingColumnFromAllReferences,
		// Token: 0x04001C51 RID: 7249
		AmbiguousColumn
	}
}
