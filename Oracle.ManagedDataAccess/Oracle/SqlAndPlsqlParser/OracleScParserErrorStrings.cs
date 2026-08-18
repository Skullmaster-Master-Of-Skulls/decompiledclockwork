using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000267 RID: 615
	internal static class OracleScParserErrorStrings
	{
		// Token: 0x060018A9 RID: 6313 RVA: 0x0010420C File Offset: 0x0010240C
		static OracleScParserErrorStrings()
		{
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.Type, "Type");
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.NullTokenOrPattern, "Tokens or pattern cannot be null.");
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.RuleTupleNullSymbols, "Rule tuple has null symbols.");
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.FollowUpTokens, "Possible follow up tokens are: \n");
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.NoParseFollowUp, "There is no possible parse follow up at the token \"{0}\" : \n {1}");
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.DifferentGrammarsTuples, "The compared rule tuples belong to different grammars");
			OracleScParserErrorStrings.s_vErrorStringDictionary.Add(ParserExceptionError.MismatchedTuplesComparison, "The compared rule tuples are either null or different types");
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00104294 File Offset: 0x00102494
		public static string GetErrorString(ParserExceptionError item)
		{
			return OracleScParserErrorStrings.s_vErrorStringDictionary[item];
		}

		// Token: 0x04001B03 RID: 6915
		private const string c_vExceptionTypeStr = "Type";

		// Token: 0x04001B04 RID: 6916
		private const string c_vNullTokenOrPattern = "Tokens or pattern cannot be null.";

		// Token: 0x04001B05 RID: 6917
		private const string c_vRuleTupleNullSymbols = "Rule tuple has null symbols.";

		// Token: 0x04001B06 RID: 6918
		private const string c_vFollowUpTokens = "Possible follow up tokens are: \n";

		// Token: 0x04001B07 RID: 6919
		private const string c_vNoParseFollowUp = "There is no possible parse follow up at the token \"{0}\" : \n {1}";

		// Token: 0x04001B08 RID: 6920
		private const string c_vDifferentGrammarsTuples = "The compared rule tuples belong to different grammars";

		// Token: 0x04001B09 RID: 6921
		private const string c_vMismatchedTuplesComparison = "The compared rule tuples are either null or different types";

		// Token: 0x04001B0A RID: 6922
		private static Dictionary<ParserExceptionError, string> s_vErrorStringDictionary = new Dictionary<ParserExceptionError, string>();
	}
}
