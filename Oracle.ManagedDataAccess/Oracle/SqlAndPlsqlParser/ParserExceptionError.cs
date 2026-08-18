using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000269 RID: 617
	public enum ParserExceptionError
	{
		// Token: 0x04001B10 RID: 6928
		Type,
		// Token: 0x04001B11 RID: 6929
		NullTokenOrPattern,
		// Token: 0x04001B12 RID: 6930
		RuleTupleNullSymbols,
		// Token: 0x04001B13 RID: 6931
		FollowUpTokens,
		// Token: 0x04001B14 RID: 6932
		NoParseFollowUp,
		// Token: 0x04001B15 RID: 6933
		DifferentGrammarsTuples,
		// Token: 0x04001B16 RID: 6934
		MismatchedTuplesComparison,
		// Token: 0x04001B17 RID: 6935
		ParseTreeBuildError
	}
}
